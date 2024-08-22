using NUnit.Framework;

#nullable enable

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class DeleteIndexTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest_1()
    {
        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/indexes/string").UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(
            async () => await Client.DeleteIndexAsync("string", RequestOptions)
        );
    }

    [Test]
    public void MockServerTest_2()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/test-index")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(
            async () => await Client.DeleteIndexAsync("test-index", RequestOptions)
        );
    }
}
