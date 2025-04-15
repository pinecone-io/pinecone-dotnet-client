using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class ListIndexesTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
    {
        const string mockResponse = """
            {
              "indexes": [
                {
                  "name": "semantic-search",
                  "dimension": 384,
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
                    "pod": {
                      "environment": "us-west1-gcp",
                      "replicas": 2,
                      "shards": 2,
                      "pod_type": "p1.x1",
                      "pods": 4,
                      "metadata_config": {
                        "indexed": [
                          "genre",
                          "title",
                          "imdb_rating"
                        ]
                      },
                      "source_collection": "movie-embeddings"
                    }
                  },
                  "status": {
                    "ready": true,
                    "state": "Ready"
                  },
                  "vector_type": "dense"
                },
                {
                  "name": "image-search",
                  "dimension": 200,
                  "metric": "dotproduct",
                  "host": "image-search-a31f9c1.svc.us-east1-gcp.pinecone.io",
                  "deletion_protection": "disabled",
                  "tags": {
                    "tag0": "val0",
                    "tag1": "val1"
                  },
                  "embed": {
                    "model": "multilingual-e5-large",
                    "metric": "cosine",
                    "dimension": 1536,
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
                      "cloud": "aws",
                      "region": "us-east-1"
                    }
                  },
                  "status": {
                    "ready": false,
                    "state": "Initializing"
                  },
                  "vector_type": "dense"
                },
                {
                  "name": "sparse-index",
                  "dimension": 1536,
                  "metric": "dotproduct",
                  "host": "sparse-index-1a2b3c4d.svc.us-east1-gcp.pinecone.io",
                  "deletion_protection": "disabled",
                  "tags": {
                    "tag0": "val0",
                    "tag1": "val1"
                  },
                  "embed": {
                    "model": "multilingual-e5-large",
                    "metric": "cosine",
                    "dimension": 1536,
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
                      "cloud": "aws",
                      "region": "us-east-1"
                    }
                  },
                  "status": {
                    "ready": true,
                    "state": "Ready"
                  },
                  "vector_type": "sparse"
                }
              ]
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/indexes").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ListIndexesAsync();
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<IndexList>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
    {
        const string mockResponse = """
            {
              "indexes": [
                {
                  "name": "movie-embeddings",
                  "dimension": 1536,
                  "metric": "cosine",
                  "host": "movie-embeddings-c01b5b5.svc.us-east1-gcp.pinecone.io",
                  "deletion_protection": "disabled",
                  "tags": {
                    "tag0": "val0",
                    "tag1": "val1"
                  },
                  "embed": {
                    "model": "multilingual-e5-large",
                    "metric": "cosine",
                    "dimension": 1536,
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
                      "cloud": "aws",
                      "region": "us-east-1"
                    }
                  },
                  "status": {
                    "ready": false,
                    "state": "Initializing"
                  },
                  "vector_type": "dense"
                }
              ]
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/indexes").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ListIndexesAsync();
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<IndexList>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_3()
    {
        const string mockResponse = """
            {
              "indexes": [
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
              ]
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/indexes").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ListIndexesAsync();
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<IndexList>(mockResponse)).UsingDefaults()
        );
    }
}
