using System.Text.Json;
using NUnit.Framework;
using Pinecone.Client.Core;

namespace Pinecone.Client.Test;

[TestFixture]
public class TestClient
{
    [SetUp]
    public void Setup()
    {
        _client = new Pinecone();
    }

    private Pinecone _client;

    [Ignore("Requires PINECONE_API_KEY")]
    [Test]
    public async Task CreateServerless()
    {
        try
        {
            var indexName = "serverless-index";
            var createIndexRequest = new CreateIndexRequest
            {
                Name = indexName,
                Dimension = 3,
                Metric = CreateIndexRequestMetric.Cosine,
                Spec = new ServerlessIndexSpec
                {
                    Serverless = new ServerlessSpec
                    {
                        Cloud = ServerlessSpecCloud.Aws,
                        Region = "us-east-1"
                    }
                },
                DeletionProtection = DeletionProtection.Disabled
            };
            var indexModel = await _client.CreateIndexAsync(createIndexRequest);
            Assert.That(indexModel, Is.Not.Null);
            Assert.That(indexModel.Name, Is.EqualTo(indexName));

            var listIndexes = await _client.ListIndexesAsync();
            Assert.That(listIndexes, Is.Not.Null);
            Assert.That(listIndexes.Indexes, Is.Not.Null);
            Assert.That(listIndexes.Indexes.ToList().Count, Is.EqualTo(1));

            var describeIndex = await _client.DescribeIndexAsync(indexName);
            Assert.That(describeIndex, Is.Not.Null);
            Assert.That(indexModel.Name, Is.EqualTo(indexName));

            // Sleep for a few seconds while the index boots up.
            Thread.Sleep(3000);
            var indexClient = _client.Index(indexName);

            var describeIndexStats = await indexClient.DescribeIndexStatsAsync(
                new DescribeIndexStatsRequest()
            );
            Assert.That(describeIndexStats, Is.Not.Null);

            var vectors = new[]
            {
                new Vector
                {
                    Id = "vector1",
                    Values = new[] { 0.1f, 0.2f, 0.3f },
                    Metadata = new Dictionary<string, MetadataValue?>
                    {
                        ["genre"] = "horror",
                        ["duration"] = 120,
                        ["extra"] = new[] { "one", "two", "three" }
                    }
                }
            };
            var upsertResponse = await indexClient.UpsertAsync(
                new UpsertRequest { Vectors = vectors, Namespace = "test" },
                new GrpcRequestOptions { Timeout = TimeSpan.FromSeconds(1) }
            );
            Assert.That(upsertResponse, Is.Not.Null);
            Assert.That(upsertResponse.UpsertedCount!, Is.EqualTo(1));

            var listResponse = await indexClient.ListAsync(new ListRequest { Namespace = "test" });
            Assert.That(listResponse, Is.Not.Null);
            Assert.That(listResponse.Vectors, Is.Not.Null);
            Assert.That(listResponse.Namespace, Is.EqualTo("test"));
        }
        catch (Exception ex)
        {
            Assert.Fail("An unexpected exception occurred: " + ex.Message);
        }
        finally
        {
            await DeleteServerless();
        }
    }

    [Ignore("Requires PINECONE_API_KEY")]
    [Test]
    public async Task DeleteServerless()
    {
        var indexName = "serverless-index";
        await _client.DeleteIndexAsync(indexName);
    }

    [Ignore("Requires PINECONE_API_KEY")]
    [Test]
    public async Task CreatePod()
    {
        try
        {
            var indexName = "pod-index";
            var createIndexRequest = new CreateIndexRequest
            {
                Name = indexName,
                Dimension = 1536,
                Metric = CreateIndexRequestMetric.Cosine,
                Spec = new PodIndexSpec
                {
                    Pod = new PodSpec
                    {
                        Environment = "us-west1-gcp",
                        PodType = "p1.x1",
                        Pods = 1,
                        Replicas = 1,
                        Shards = 1,
                        MetadataConfig = new PodSpecMetadataConfig
                        {
                            Indexed = new List<string> { "id" }
                        }
                    }
                },
                DeletionProtection = DeletionProtection.Disabled
            };
            var indexModel = await _client.CreateIndexAsync(createIndexRequest);
            Assert.That(indexModel, Is.Not.Null);
            Assert.That(indexModel.Name, Is.EqualTo(indexName));

            // Sleep for a few seconds while the index boots up.
            Thread.Sleep(5000);

            var listIndexes = await _client.ListIndexesAsync();
            Assert.That(listIndexes, Is.Not.Null);
            Assert.That(listIndexes.Indexes, Is.Not.Null);
            Assert.That(listIndexes.Indexes.ToList().Count, Is.EqualTo(1));

            var describeIndex = await _client.DescribeIndexAsync(indexName);
            Assert.That(describeIndex, Is.Not.Null);

            var configureIndex = await _client.ConfigureIndexAsync(
                indexName,
                new ConfigureIndexRequest
                {
                    Spec = new ConfigureIndexRequestSpec
                    {
                        Pod = new ConfigureIndexRequestSpecPod { Replicas = 2, PodType = "p1.x1" }
                    }
                }
            );
            Assert.That(indexModel, Is.Not.Null);
            Assert.That(configureIndex.Name, Is.EqualTo(indexName));
        }
        catch (Exception ex)
        {
            Assert.Fail("An unexpected exception occurred: " + ex.Message);
        }
        finally
        {
            DeletePod();
        }
    }

    [Ignore("Requires PINECONE_API_KEY")]
    [Test]
    public async Task DeletePod()
    {
        var indexName = "pod-index";
        await _client.DeleteIndexAsync(indexName);
    }
}
