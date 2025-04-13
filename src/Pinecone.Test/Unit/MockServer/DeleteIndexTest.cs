using NUnit.Framework;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class DeleteIndexTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/test-index")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () => await Client.DeleteIndexAsync("test-index"));
    }
}
