using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class ListCollectionsTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
    {
        const string mockResponse = """
            {
              "collections": [
                {
                  "name": "small-collection",
                  "size": 3126700,
                  "status": "Ready",
                  "dimension": 3,
                  "vector_count": 99,
                  "environment": "us-east1-gcp"
                },
                {
                  "name": "small-collection-new",
                  "size": 3126700,
                  "status": "Initializing",
                  "dimension": 3,
                  "vector_count": 99,
                  "environment": "us-east1-gcp"
                },
                {
                  "name": "big-collection",
                  "size": 160087040000000,
                  "status": "Ready",
                  "dimension": 1536,
                  "vector_count": 10000000,
                  "environment": "us-east1-gcp"
                }
              ]
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/collections").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ListCollectionsAsync();
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<CollectionList>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
    {
        const string mockResponse = """
            {
              "collections": [
                {
                  "name": "example-collection",
                  "size": 10000000,
                  "status": "Initializing",
                  "dimension": 1536,
                  "vector_count": 120000,
                  "environment": "us-east1-gcp"
                }
              ]
            }
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/collections").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ListCollectionsAsync();
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<CollectionList>(mockResponse)).UsingDefaults()
        );
    }
}
