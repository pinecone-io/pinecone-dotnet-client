using System.Threading.Tasks;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

#nullable enable

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class CreateCollectionTest : BaseMockServerTest
{
    [Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "name": "example-collection",
              "source": "example-source-index"
            }
            """;

        const string mockResponse = """
            {
              "name": "example-collection",
              "size": 10000000,
              "status": "Initializing",
              "dimension": 1536,
              "vector_count": 120000,
              "environment": "us-east1-gcp"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/collections")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.CreateCollectionAsync(
            new CreateCollectionRequest
            {
                Name = "example-collection",
                Source = "example-source-index",
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
              "name": "example-collection",
              "source": "example-source-index"
            }
            """;

        const string mockResponse = """
            {
              "name": "example-collection",
              "size": 10000000,
              "status": "Initializing",
              "dimension": 1536,
              "vector_count": 120000,
              "environment": "us-east1-gcp"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/collections")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.CreateCollectionAsync(
            new CreateCollectionRequest
            {
                Name = "example-collection",
                Source = "example-source-index",
            },
            RequestOptions
        );
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }
}
