using System.Threading.Tasks;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone.Core;

#nullable enable

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class DescribeIndexTest : BaseMockServerTest
{
    [Test]
    public async Task MockServerTest_1()
    {
        const string mockResponse = """
            {
              "name": "movie-recommendations",
              "dimension": 1536,
              "metric": "cosine",
              "host": "movie-recommendations-c01b5b5.svc.us-east1-gcp.pinecone.io",
              "deletion_protection": "disabled",
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
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/indexes/string").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.DescribeIndexAsync("string", RequestOptions);
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
              "name": "movie-recommendations",
              "dimension": 1536,
              "metric": "cosine",
              "host": "movie-recommendations-c01b5b5.svc.us-east1-gcp.pinecone.io",
              "deletion_protection": "disabled",
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
              },
              "status": {
                "ready": false,
                "state": "Initializing"
              }
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/indexes/string").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.DescribeIndexAsync("string", RequestOptions);
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
              "name": "movie-recommendations",
              "dimension": 1536,
              "metric": "cosine",
              "host": "movie-recommendations-c01b5b5.svc.us-east1-gcp.pinecone.io",
              "deletion_protection": "disabled",
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
            """;

        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/indexes/test-index").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.DescribeIndexAsync("test-index", RequestOptions);
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
              "name": "movie-recommendations",
              "dimension": 1536,
              "metric": "cosine",
              "host": "movie-recommendations-c01b5b5.svc.us-east1-gcp.pinecone.io",
              "deletion_protection": "disabled",
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
              },
              "status": {
                "ready": false,
                "state": "Initializing"
              }
            }
            """;

        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/indexes/test-index").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.DescribeIndexAsync("test-index", RequestOptions);
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }
}
