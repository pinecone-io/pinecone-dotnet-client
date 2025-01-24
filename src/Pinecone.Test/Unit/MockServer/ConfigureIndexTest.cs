using System.Threading.Tasks;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

#nullable enable

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class ConfigureIndexTest : BaseMockServerTest
{
    [Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            {}
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
                "vector_type": "vector_type",
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
              "vector_type": "vector_type"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/index_name")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ConfigureIndexAsync(
            "index_name",
            new ConfigureIndexRequest
            {
                Spec = null,
                DeletionProtection = null,
                Tags = null,
                Embed = null,
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
              "spec": {
                "pod": {
                  "pod_type": "p1.x2"
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
                "vector_type": "vector_type",
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
              "vector_type": "vector_type"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/test-index")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ConfigureIndexAsync(
            "test-index",
            new ConfigureIndexRequest
            {
                Spec = new ConfigureIndexRequestSpec
                {
                    Pod = new ConfigureIndexRequestSpecPod { PodType = "p1.x2" },
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
    public async Task MockServerTest_3()
    {
        const string requestJson = """
            {
              "spec": {
                "pod": {
                  "replicas": 4
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
                "vector_type": "vector_type",
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
              "vector_type": "vector_type"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/test-index")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ConfigureIndexAsync(
            "test-index",
            new ConfigureIndexRequest
            {
                Spec = new ConfigureIndexRequestSpec
                {
                    Pod = new ConfigureIndexRequestSpecPod { Replicas = 4 },
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
    public async Task MockServerTest_4()
    {
        const string requestJson = """
            {
              "spec": {
                "pod": {
                  "replicas": 4,
                  "pod_type": "p1.x2"
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
                "vector_type": "vector_type",
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
              "vector_type": "vector_type"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/test-index")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ConfigureIndexAsync(
            "test-index",
            new ConfigureIndexRequest
            {
                Spec = new ConfigureIndexRequestSpec
                {
                    Pod = new ConfigureIndexRequestSpecPod { Replicas = 4, PodType = "p1.x2" },
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
    public async Task MockServerTest_5()
    {
        const string requestJson = """
            {}
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
                "vector_type": "vector_type",
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
              "vector_type": "vector_type"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/test-index")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ConfigureIndexAsync(
            "test-index",
            new ConfigureIndexRequest(),
            RequestOptions
        );
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }

    [Test]
    public async Task MockServerTest_6()
    {
        const string requestJson = """
            {
              "tags": {
                "tag0": "new-val",
                "tag1": ""
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
                "vector_type": "vector_type",
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
              "vector_type": "vector_type"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/test-index")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ConfigureIndexAsync(
            "test-index",
            new ConfigureIndexRequest
            {
                Tags = new Dictionary<string, string>() { { "tag0", "new-val" }, { "tag1", "" } },
            },
            RequestOptions
        );
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }
}
