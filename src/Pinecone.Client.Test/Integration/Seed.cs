using NUnit.Framework;
using Pinecone.Client.Core;

namespace Pinecone.Client.Test.Integration;

[TestFixture]
public class PineconeTests
{
    [SetUp]
    public void Setup()
    {
        var rawClient = new RawClient(); // Initialize as per your configuration
        _indexClient = new IndexClient(rawClient);
    }

    private IndexClient _indexClient;

    private async Task SetupData(IndexClient idx, string targetNamespace, bool wait)
    {
        // Upsert without metadata
        await idx.UpsertAsync(new UpsertRequest
        {
            Vectors = new List<Vector>
            {
                new() { Id = "1", Values = EmbeddingValues(2) },
                new() { Id = "2", Values = EmbeddingValues(2) },
                new() { Id = "3", Values = EmbeddingValues(2) }
            },
            Namespace = targetNamespace
        });

        // Upsert with metadata
        await idx.UpsertAsync(new UpsertRequest
        {
            Vectors = new List<Vector>
            {
                new()
                {
                    Id = "4", Values = EmbeddingValues(2),
                    Metadata = new Dictionary<string, MetadataValue?> { { "genre", "action" }, { "runtime", 120 } }
                },
                new()
                {
                    Id = "5", Values = EmbeddingValues(2),
                    Metadata = new Dictionary<string, MetadataValue?> { { "genre", "comedy" }, { "runtime", 90 } }
                },
                new()
                {
                    Id = "6", Values = EmbeddingValues(2),
                    Metadata = new Dictionary<string, MetadataValue?> { { "genre", "romance" }, { "runtime", 240 } }
                }
            },
            Namespace = targetNamespace
        });

        // Upsert with dict
        await idx.UpsertAsync(new UpsertRequest
        {
            Vectors = new List<Vector>
            {
                new() { Id = "7", Values = EmbeddingValues(2) },
                new() { Id = "8", Values = EmbeddingValues(2) },
                new() { Id = "9", Values = EmbeddingValues(2) }
            },
            Namespace = targetNamespace
        });

        if (wait)
            await PollFetchForIdsInNamespace(idx, ["1", "2", "3", "4", "5", "6", "7", "8", "9"],
                targetNamespace);
    }

    private async Task PollFetchForIdsInNamespace(IndexClient idx, string[] ids, string targetNamespace)
    {
        // Implement polling logic as per your requirement.
    }

    private float[] EmbeddingValues(int count)
    {
        return new float[count];
    }
}