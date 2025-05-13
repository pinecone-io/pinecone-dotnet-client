using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class CreateIndexFromBackupTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "name": "example-index"
            }
            """;

        const string mockResponse = """
            {
              "restore_job_id": "670e8400-e29b-41d4-a716-446655440000",
              "index_id": "123e4567-e89b-12d3-a456-426614174000"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/backups/670e8400-e29b-41d4-a716-446655440000/create-index")
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

        var response = await Client.Backups.CreateIndexFromBackupAsync(
            "670e8400-e29b-41d4-a716-446655440000",
            new CreateIndexFromBackupRequest { Name = "example-index" }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<CreateIndexFromBackupResponse>(mockResponse))
                .UsingDefaults()
        );
    }
}
