namespace Pinecone.Client.Test.Integration;

public class Seed
{
    public static async Task SetupData(IndexClient idx, string targetNamespace, bool wait)
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

    public static async Task SetupListData(IndexClient idx, string targetNamespace, bool wait)
    {
        // Upsert a bunch more stuff for testing list pagination
        for (var i = 0; i < 1000; i += 50)
        {
            var vectors = new List<Vector>();
            for (var d = 0; d < 50; d++)
                vectors.Add(new Vector { Id = (i + d).ToString(), Values = EmbeddingValues(2) });

            await idx.UpsertAsync(new UpsertRequest { Vectors = vectors, Namespace = targetNamespace });
        }

        if (wait) await PollFetchForIdsInNamespace(idx, new[] { "999" }, targetNamespace);
    }

    private static async Task PollFetchForIdsInNamespace(IndexClient idx, string[] ids, string targetNamespace)
    {
        // Implement polling logic as per your requirement.
    }

    private static float[] EmbeddingValues(int count)
    {
        return new float[count];
    }
}