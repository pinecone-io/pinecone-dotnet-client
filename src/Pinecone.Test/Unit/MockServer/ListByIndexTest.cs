using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class ListByIndexTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": [
                {
                  "backup_id": "bkp_123abc",
                  "source_index_name": "my-index",
                  "source_index_id": "idx_456",
                  "name": "backup_2024_03_15",
                  "description": "Monthly backup of production index",
                  "status": "Ready",
                  "cloud": "aws",
                  "region": "us-east-1",
                  "dimension": 1536,
                  "metric": "cosine",
                  "record_count": 120000,
                  "namespace_count": 3,
                  "size_bytes": 10000000,
                  "tags": {
                    "environment": "production",
                    "type": "monthly"
                  },
                  "created_at": "2024-03-15T10:30:00.000Z"
                },
                {
                  "backup_id": "bkp_789xyz",
                  "source_index_name": "my-index",
                  "source_index_id": "idx_456",
                  "name": "backup_2024_03_20",
                  "description": "Pre-deployment safety backup",
                  "status": "Ready",
                  "cloud": "aws",
                  "region": "us-east-1",
                  "dimension": 1536,
                  "metric": "cosine",
                  "record_count": 125000,
                  "namespace_count": 4,
                  "size_bytes": 10500000,
                  "tags": {
                    "environment": "production",
                    "type": "pre-deploy"
                  },
                  "created_at": "2024-03-20T15:45:00.000Z"
                }
              ],
              "pagination": {
                "next": "dXNlcl9pZD11c2VyXzE="
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/index_name/backups")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Backups.ListByIndexAsync(
            "index_name",
            new ListBackupsByIndexRequest()
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<BackupList>(mockResponse)).UsingDefaults()
        );
    }
}
