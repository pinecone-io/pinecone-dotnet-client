using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class DescribeCollectionTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "name": "tiny-collection",
              "size": 3126700,
              "status": "Ready",
              "dimension": 3,
              "vector_count": 99,
              "environment": "us-east1-gcp"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/collections/tiny-collection")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.DescribeCollectionAsync("tiny-collection");
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<CollectionModel>(mockResponse)).UsingDefaults()
        );
    }
}
