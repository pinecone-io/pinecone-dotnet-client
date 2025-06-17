using NUnit.Framework;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class DeleteTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/backups/670e8400-e29b-41d4-a716-446655440000")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Backups.DeleteAsync("670e8400-e29b-41d4-a716-446655440000")
        );
    }
}
