using NUnit.Framework;

namespace Pinecone.Test.Integration.Control;

public class TestCollectionsHappyPath : BaseTest
{
    [Test]
    public async Task TestIndexToCollectionToIndexHappyPath()
    {
        var index = Client.Index(IndexName);
        var numVectors = 10;
        var vectors = Enumerable
            .Range(0, numVectors)
            .Select(i => new Vector
            {
                Id = i.ToString(),
                Values = Helpers.EmbeddingValues(Dimension)
            })
            .ToList();

        await index.UpsertAsync(new UpsertRequest { Vectors = vectors });

        var collectionName = $"coll1-{Helpers.RandomString(10)}";

        await Helpers.CreateCollectionAndWaitUntilReady(Client, collectionName, IndexName);

        var desc = await Client.DescribeCollectionAsync(collectionName);

        Assert.That(desc.Name, Is.EqualTo(collectionName));
        Assert.That(desc.Status, Is.EqualTo(CollectionModelStatus.Ready));
        Assert.That(desc.Environment, Is.EqualTo(Environment));
        Assert.That(desc.Dimension, Is.EqualTo(Dimension));
        Assert.Greater(desc.VectorCount, 0);
        Assert.IsNotNull(desc.Size);
        Assert.Greater(desc.Size, 0);

        // Create index from collection
        var newIndexName = $"index-from-collection-{collectionName}";
        Console.WriteLine($"Creating index {newIndexName} from collection {collectionName}...");
        await Helpers.CreatePodIndexAndWaitUntilReady(
            Client,
            newIndexName,
            Environment,
            Dimension,
            Metric,
            sourceCollection: collectionName
        );
        var newIndex = Client.Index(newIndexName);

        // Verify stats reflect the vectors present in the collection
        var stats = await newIndex.DescribeIndexStatsAsync(new DescribeIndexStatsRequest());
        Assert.That(stats.TotalVectorCount, Is.EqualTo(numVectors));

        // Verify the vectors from the collection can be fetched
        var results = await newIndex.FetchAsync(
            new FetchRequest { Ids = vectors.Select(v => v.Id).ToArray() }
        );
        foreach (var v in vectors)
        {
            var fetchedVector = results.Vectors![v.Id];
            Assert.That(fetchedVector.Id, Is.EqualTo(v.Id));
            Assert.That(
                fetchedVector.Values.ToArray(),
                Is.EqualTo(v.Values.ToArray()).Within(0.01).Percent
            );
        }
    }

    [Test]
    public async Task TestCreateIndexWithDifferentMetricFromOrigIndex()
    {
        var metrics = new[]
        {
            CreateIndexRequestMetric.Cosine,
            CreateIndexRequestMetric.Euclidean,
            CreateIndexRequestMetric.Dotproduct
        };
        var targetMetric = metrics.FirstOrDefault(m => m != Metric);

        var indexName = $"from-coll-{Helpers.RandomString(10)}";
        await Client.CreateIndexAsync(
            new CreateIndexRequest
            {
                Name = indexName,
                Dimension = Dimension,
                Metric = targetMetric,
                Spec = new PodIndexSpec
                {
                    Pod = new PodSpec
                    {
                        Environment = Environment,
                        SourceCollection = CollectionName,
                        PodType = "p1.x1",
                        Replicas = 1,
                        Shards = 1,
                        Pods = 1
                    }
                }
            }
        );
    }
}
