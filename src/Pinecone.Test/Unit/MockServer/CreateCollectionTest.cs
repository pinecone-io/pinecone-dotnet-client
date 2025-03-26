using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class CreateCollectionTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
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

        var response = await Client.CreateCollectionAsync(
            new CreateCollectionRequest
            {
                Name = "example-collection",
                Source = "example-source-index",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<CollectionModel>(mockResponse)).UsingDefaults()
        );
    }
}
