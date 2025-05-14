using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class CreateIndexTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
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
                Metric = MetricType.Cosine,
                DeletionProtection = DeletionProtection.Enabled,
                Spec = new ServerlessIndexSpec
                {
                    Serverless = new ServerlessSpec
                    {
                        Cloud = ServerlessSpecCloud.Gcp,
                        Region = "us-east1",
                    },
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
                Metric = MetricType.Dotproduct,
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
                Metric = MetricType.Cosine,
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
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<Index>(mockResponse)).UsingDefaults()
        );
    }
}
