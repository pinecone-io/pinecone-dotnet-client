using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class BackupIndexTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
    {
        const string requestJson = """
            {}
            """;

        const string mockResponse = """
            {
              "backup_id": "670e8400-e29b-41d4-a716-446655440001",
              "source_index_name": "my-index",
              "source_index_id": "670e8400-e29b-41d4-a716-446655440000",
              "name": "backup-2025-02-04",
              "description": "Backup before bulk update.",
              "status": "Ready",
              "cloud": "aws",
              "region": "us-east-1",
              "dimension": 1536,
              "metric": "cosine",
              "record_count": 120000,
              "namespace_count": 3,
              "size_bytes": 10000000,
              "tags": {
                "tag0": "val0",
                "tag1": "val1"
              },
              "created_at": "created_at"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/index_name/backups")
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

        var response = await Client.Backups.BackupIndexAsync(
            "index_name",
            new BackupIndexRequest()
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<BackupModel>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
    {
        const string requestJson = """
            {
              "name": "backup-index"
            }
            """;

        const string mockResponse = """
            {
              "backup_id": "670e8400-e29b-41d4-a716-446655440001",
              "source_index_name": "my-index",
              "source_index_id": "670e8400-e29b-41d4-a716-446655440000",
              "name": "backup-2025-02-04",
              "description": "Backup before bulk update.",
              "status": "Ready",
              "cloud": "aws",
              "region": "us-east-1",
              "dimension": 1536,
              "metric": "cosine",
              "record_count": 120000,
              "namespace_count": 3,
              "size_bytes": 10000000,
              "tags": {
                "tag0": "val0",
                "tag1": "val1"
              },
              "created_at": "created_at"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/index_name/backups")
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

        var response = await Client.Backups.BackupIndexAsync(
            "index_name",
            new BackupIndexRequest { Name = "backup-index" }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<BackupModel>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_3()
    {
        const string requestJson = """
            {
              "name": "backup-index",
              "description": "Backup of the index"
            }
            """;

        const string mockResponse = """
            {
              "backup_id": "670e8400-e29b-41d4-a716-446655440001",
              "source_index_name": "my-index",
              "source_index_id": "670e8400-e29b-41d4-a716-446655440000",
              "name": "backup-2025-02-04",
              "description": "Backup before bulk update.",
              "status": "Ready",
              "cloud": "aws",
              "region": "us-east-1",
              "dimension": 1536,
              "metric": "cosine",
              "record_count": 120000,
              "namespace_count": 3,
              "size_bytes": 10000000,
              "tags": {
                "tag0": "val0",
                "tag1": "val1"
              },
              "created_at": "created_at"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes/index_name/backups")
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

        var response = await Client.Backups.BackupIndexAsync(
            "index_name",
            new BackupIndexRequest { Name = "backup-index", Description = "Backup of the index" }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<BackupModel>(mockResponse)).UsingDefaults()
        );
    }
}
