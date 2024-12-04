using System.Threading.Tasks;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone.Core;

#nullable enable

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class ListIndexesTest : BaseMockServerTest
{
    [Test]
    public async Task MockServerTest_1()
    {
        const string mockResponse = """
            {
              "indexes": [
                {
                  "name": "x",
                  "dimension": 20000,
                  "metric": "cosine",
                  "host": "host",
                  "deletion_protection": "disabled",
                  "tags": {
                    "tags": "tags"
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
                  }
                },
                {
                  "name": "x",
                  "dimension": 20000,
                  "metric": "cosine",
                  "host": "host",
                  "deletion_protection": "disabled",
                  "tags": {
                    "tags": "tags"
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
                  }
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

        var response = await Client.ListIndexesAsync(RequestOptions);
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }

    [Test]
    public async Task MockServerTest_2()
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
                  }
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
                  "spec": {
                    "serverless": {
                      "cloud": "aws",
                      "region": "us-east-1"
                    }
                  },
                  "status": {
                    "ready": false,
                    "state": "Initializing"
                  }
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

        var response = await Client.ListIndexesAsync(RequestOptions);
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }

    [Test]
    public async Task MockServerTest_3()
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
                  "spec": {
                    "serverless": {
                      "cloud": "aws",
                      "region": "us-east-1"
                    }
                  },
                  "status": {
                    "ready": false,
                    "state": "Initializing"
                  }
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

        var response = await Client.ListIndexesAsync(RequestOptions);
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }

    [Test]
    public async Task MockServerTest_4()
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
                  "spec": {
                    "serverless": {
                      "cloud": "gcp",
                      "region": "us-east-1"
                    }
                  },
                  "status": {
                    "ready": true,
                    "state": "ScalingUpPodSize"
                  }
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

        var response = await Client.ListIndexesAsync(RequestOptions);
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }
}
