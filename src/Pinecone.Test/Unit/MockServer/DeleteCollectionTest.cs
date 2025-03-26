using NUnit.Framework;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class DeleteCollectionTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/collections/test-collection")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () => await Client.DeleteCollectionAsync("test-collection"));
    }
}
