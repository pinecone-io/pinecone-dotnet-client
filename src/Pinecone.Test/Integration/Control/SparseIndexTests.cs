using System.Text.Json;
using NUnit.Framework;

namespace Pinecone.Test.Integration.Control;

public class SparseIndexTests : BaseTest
{
    [Test]
    public async Task TestCreateSparseIndexAsync()
    {
        try
        {
            // Define the index name and dimensions
            var indexName = Helpers.GenerateIndexName("sparse-index-testing");

            // Create the sparse index
            var index = await Client.CreateIndexAsync(new CreateIndexRequest
            {
                Name = indexName,
                Metric = CreateIndexRequestMetric.Dotproduct,
                VectorType = VectorType.Sparse,
                Spec = new PodIndexSpec
                {
                    Pod = new PodSpec
                    {
                        Environment = "us-west1-gcp",
                        PodType = "p1.x1",
                        Pods = 1,
                        Replicas = 1,
                        Shards = 1,
                    }
                },
                DeletionProtection = DeletionProtection.Enabled
            }).ConfigureAwait(false);

            // Verify the index was created successfully
            Assert.That(index, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(index.Name, Is.EqualTo(indexName));
                Assert.That(index.Metric, Is.EqualTo(IndexModelMetric.Cosine));
                Assert.That(index.VectorType, Is.EqualTo(VectorType.Sparse));
            });
        }
        catch (PineconeApiException e)
        {
            TestContext.WriteLine(e.Message);
            TestContext.WriteLine(e.Body is string ? e.Body : JsonSerializer.Serialize(e.Body));
            throw;
        }
    }
}
