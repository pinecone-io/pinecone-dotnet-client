using System.Threading.Tasks;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

#nullable enable

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class CreateIndexTest : BaseMockServerTest
{
    [Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "name": "x",
              "spec": {
                "serverless": {
                  "cloud": "gcp",
                  "region": "region"
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
                    .WithPath("/indexes")
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

        var response = await Client.CreateIndexAsync(
            new CreateIndexRequest
            {
                Name = "x",
                Dimension = null,
                Metric = null,
                DeletionProtection = null,
                Tags = null,
                Spec = new ServerlessIndexSpec
                {
                    Serverless = new ServerlessSpec
                    {
                        Cloud = ServerlessSpecCloud.Gcp,
                        Region = "region",
                    },
                },
                VectorType = null,
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
              "name": "movie-recommendations",
              "dimension": 1536,
              "metric": "cosine",
              "deletion_protection": "enabled",
              "spec": {
                "serverless": {
                  "cloud": "gcp",
                  "region": "us-east1"
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
                    .WithPath("/indexes")
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

        var response = await Client.CreateIndexAsync(
            new CreateIndexRequest
            {
                Name = "movie-recommendations",
                Dimension = 1536,
                Metric = CreateIndexRequestMetric.Cosine,
                DeletionProtection = DeletionProtection.Enabled,
                Spec = new ServerlessIndexSpec
                {
                    Serverless = new ServerlessSpec
                    {
                        Cloud = ServerlessSpecCloud.Gcp,
                        Region = "us-east1",
                    },
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
              "name": "sparse-index",
              "metric": "dotproduct",
              "deletion_protection": "enabled",
              "spec": {
                "serverless": {
                  "cloud": "gcp",
                  "region": "us-east1"
                }
              },
              "vector_type": "sparse"
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
                    .WithPath("/indexes")
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

        var response = await Client.CreateIndexAsync(
            new CreateIndexRequest
            {
                Name = "sparse-index",
                Metric = CreateIndexRequestMetric.Dotproduct,
                DeletionProtection = DeletionProtection.Enabled,
                Spec = new ServerlessIndexSpec
                {
                    Serverless = new ServerlessSpec
                    {
                        Cloud = ServerlessSpecCloud.Gcp,
                        Region = "us-east1",
                    },
                },
                VectorType = VectorType.Sparse,
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
              "name": "movie-recommendations",
              "dimension": 1536,
              "metric": "cosine",
              "deletion_protection": "enabled",
              "spec": {
                "pod": {
                  "environment": "us-east-1-aws",
                  "replicas": 1,
                  "shards": 1,
                  "pod_type": "p1.x1",
                  "pods": 1,
                  "metadata_config": {
                    "indexed": [
                      "genre",
                      "title",
                      "imdb_rating"
                    ]
                  },
                  "source_collection": "movie-embeddings"
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
                    .WithPath("/indexes")
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

        var response = await Client.CreateIndexAsync(
            new CreateIndexRequest
            {
                Name = "movie-recommendations",
                Dimension = 1536,
                Metric = CreateIndexRequestMetric.Cosine,
                DeletionProtection = DeletionProtection.Enabled,
                Spec = new PodIndexSpec
                {
                    Pod = new PodSpec
                    {
                        Environment = "us-east-1-aws",
                        Replicas = 1,
                        Shards = 1,
                        PodType = "p1.x1",
                        Pods = 1,
                        MetadataConfig = new PodSpecMetadataConfig
                        {
                            Indexed = new List<string>() { "genre", "title", "imdb_rating" },
                        },
                        SourceCollection = "movie-embeddings",
                    },
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
