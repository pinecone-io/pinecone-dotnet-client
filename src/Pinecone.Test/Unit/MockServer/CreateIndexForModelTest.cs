using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class CreateIndexForModelTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "name": "multilingual-e5-large-index",
              "cloud": "gcp",
              "region": "us-east1",
              "deletion_protection": "enabled",
              "embed": {
                "model": "multilingual-e5-large",
                "metric": "cosine",
                "field_map": {
                  "text": "your-text-field"
                }
              }
            }
            """;

        const string mockResponse = """
            {
              "name": "example-index",
              "dimension": 1536,
              "metric": "cosine",
              "host": "semantic-search-c01b5b5.svc.us-west1-gcp.pinecone.io",
              "deletion_protection": "disabled",
              "tags": {
                "tag0": "val0",
                "tag1": "val1"
              },
              "embed": {
                "model": "multilingual-e5-large",
                "metric": "cosine",
                "dimension": 1536,
                "vector_type": "dense",
                "field_map": {
                  "text": "your-text-field"
                },
                "read_parameters": {
                  "input_type": "query",
                  "truncate": "NONE"
                },
                "write_parameters": {
                  "input_type": "passage"
                }
              },
              "spec": {
                "serverless": {
                  "cloud": "gcp",
                  "region": "us-east-1"
                }
              },
              "status": {
                "ready": true,
                "state": "ScalingUpPodSize"
              },
              "vector_type": "dense"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/create-for-model")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.CreateIndexForModelAsync(
            new CreateIndexForModelRequest
            {
                Name = "multilingual-e5-large-index",
                Cloud = CreateIndexForModelRequestCloud.Gcp,
                Region = "us-east1",
                DeletionProtection = DeletionProtection.Enabled,
                Embed = new CreateIndexForModelRequestEmbed
                {
                    Model = "multilingual-e5-large",
                    Metric = MetricType.Cosine,
                    FieldMap = new Dictionary<string, object>() { { "text", "your-text-field" } },
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Index>(mockResponse)).UsingDefaults()
        );
    }
}
