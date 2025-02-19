using NUnit.Framework;

#nullable enable

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class DeleteCollectionTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest_1()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/collections/collection_name")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(
            async () => await Client.DeleteCollectionAsync("collection_name", RequestOptions)
        );
    }

    [Test]
    public void MockServerTest_2()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/collections/test-collection")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(
            async () => await Client.DeleteCollectionAsync("test-collection", RequestOptions)
        );
    }
}
