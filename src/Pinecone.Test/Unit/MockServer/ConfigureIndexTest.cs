using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class ConfigureIndexTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
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
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Index>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
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
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Index>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_3()
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
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Index>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_4()
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

        var response = await Client.ConfigureIndexAsync("test-index", new ConfigureIndexRequest());
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Index>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_5()
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
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Index>(mockResponse)).UsingDefaults()
        );
    }
}
