using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

public class TestSetupUpsert : BaseTest
{
    [TestCase(true)]
    [TestCase(false)]
    public async Task TestUpsertToNamespace(bool useNondefaultNamespace)
    {
        var indexClient = await CreateIndexForTest();
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        // Upsert with tuples
        await indexClient.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new() { Id = "1", Values = Helpers.EmbeddingValues() },
                    new() { Id = "2", Values = Helpers.EmbeddingValues() },
                    new() { Id = "3", Values = Helpers.EmbeddingValues() }
                },
                Namespace = targetNamespace
            }
        );

        // Upsert with objects
        await indexClient.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new() { Id = "4", Values = Helpers.EmbeddingValues() },
                    new() { Id = "5", Values = Helpers.EmbeddingValues() },
                    new() { Id = "6", Values = Helpers.EmbeddingValues() }
                },
                Namespace = targetNamespace
            }
        );

        // Upsert with dictionary
        await indexClient.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new() { Id = "7", Values = Helpers.EmbeddingValues() },
                    new() { Id = "8", Values = Helpers.EmbeddingValues() },
                    new() { Id = "9", Values = Helpers.EmbeddingValues() }
                },
                Namespace = targetNamespace
            }
        );

        await Helpers.PollStatsForNamespaceAsync(indexClient, targetNamespace, 9);

        // Check the vector count reflects that some data has been upserted
        var stats = await indexClient.DescribeIndexStatsAsync(new DescribeIndexStatsRequest());
        Assert.That(stats.TotalVectorCount, Is.EqualTo(9));
        Assert.That(stats.Namespaces![targetNamespace].VectorCount, Is.EqualTo(9));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestUpsertToNamespaceWithSparseEmbeddingValues(bool useNondefaultNamespace)
    {
        var indexClient = await CreateIndexForTest();
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        // Upsert with sparse values object
        await indexClient.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new()
                    {
                        Id = "1",
                        Values = Helpers.EmbeddingValues(),
                        SparseValues = new SparseValues
                        {
                            Indices = new List<uint> { 0, 1 },
                            Values = Helpers.EmbeddingValues()
                        }
                    }
                },
                Namespace = targetNamespace
            }
        );

        // Upsert with sparse values dictionary
        await indexClient.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new()
                    {
                        Id = "2",
                        Values = Helpers.EmbeddingValues(),
                        SparseValues = new SparseValues
                        {
                            Indices = new List<uint> { 0, 1 },
                            Values = Helpers.EmbeddingValues()
                        }
                    },
                    new()
                    {
                        Id = "3",
                        Values = Helpers.EmbeddingValues(),
                        SparseValues = new SparseValues
                        {
                            Indices = new List<uint> { 0, 1 },
                            Values = Helpers.EmbeddingValues()
                        }
                    }
                },
                Namespace = targetNamespace
            }
        );

        await Helpers.PollStatsForNamespaceAsync(indexClient, targetNamespace, 3);

        // Check the vector count reflects that some data has been upserted
        var stats = await indexClient.DescribeIndexStatsAsync(new DescribeIndexStatsRequest());
        Assert.That(stats.TotalVectorCount, Is.EqualTo(3));
        Assert.That(stats.Namespaces![targetNamespace].VectorCount, Is.EqualTo(3));
    }

    private async Task<IndexClient> CreateIndexForTest()
    {
        var indexName = Helpers.GenerateIndexName("upsert-testing");
        await Client.CreateIndexAsync(
            new CreateIndexRequest
            {
                Name = indexName,
                Dimension = 2,
                Metric = CreateIndexRequestMetric.Cosine,
                Spec = new ServerlessIndexSpec
                {
                    Serverless = new ServerlessSpec
                    {
                        Cloud = ServerlessSpecCloud.Aws,
                        Region = "us-east-1"
                    }
                }
            }
        );
        await Task.Delay(5000);
        return Client.Index(indexName);
    }
}
