using System.Threading.Tasks;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

#nullable enable

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class CreateIndexForModelTest : BaseMockServerTest
{
    [Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "name": "x",
              "cloud": "gcp",
              "region": "region",
              "embed": {
                "model": "model",
                "field_map": {
                  "field_map": {
                    "key": "value"
                  }
                }
              }
            }
            """;

        const string mockResponse = """
            {
              "name": "x",
              "dimension": 20000,
              "metric": "cosine",
              "host": "host",
              "deletion_protection": "disabled",
              "tags": {
                "tags": "tags"
              },
              "embed": {
                "model": "model",
                "metric": "cosine",
                "dimension": 20000,
                "vector_type": "dense",
                "field_map": {
                  "field_map": {
                    "key": "value"
                  }
                },
                "read_parameters": {
                  "read_parameters": {
                    "key": "value"
                  }
                },
                "write_parameters": {
                  "write_parameters": {
                    "key": "value"
                  }
                }
              },
              "spec": {
                "serverless": {
                  "cloud": "gcp",
                  "region": "region"
                }
              },
              "status": {
                "ready": true,
                "state": "Initializing"
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
                Name = "x",
                Cloud = CreateIndexForModelRequestCloud.Gcp,
                Region = "region",
                DeletionProtection = null,
                Tags = null,
                Embed = new CreateIndexForModelRequestEmbed
                {
                    Model = "model",
                    Metric = null,
                    FieldMap = new Dictionary<string, object>()
                    {
                        {
                            "field_map",
                            new Dictionary<object, object?>() { { "key", "value" } }
                        },
                    },
                    ReadParameters = null,
                    WriteParameters = null,
                },
            },
            RequestOptions
        );
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }

    [Test]
    public async Task MockServerTest_2()
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
                    Metric = CreateIndexForModelRequestEmbedMetric.Cosine,
                    FieldMap = new Dictionary<string, object>() { { "text", "your-text-field" } },
                },
            },
            RequestOptions
        );
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }
}
