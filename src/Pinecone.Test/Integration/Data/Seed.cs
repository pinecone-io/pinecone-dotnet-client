namespace Pinecone.Test.Integration.Data;

public static class Seed
{
    public static async Task SetupData(IndexClient idx, string targetNamespace, bool wait)
    {
        // Upsert without metadata
        await idx.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new() { Id = "1", Values = Helpers.EmbeddingValues(2) },
                    new() { Id = "2", Values = Helpers.EmbeddingValues(2) },
                    new() { Id = "3", Values = Helpers.EmbeddingValues(2) }
                },
                Namespace = targetNamespace
            }
        );

        // Upsert with metadata
        await idx.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new()
                    {
                        Id = "4",
                        Values = Helpers.EmbeddingValues(2),
                        Metadata = new Metadata { { "genre", "action" }, { "runtime", 120 } }
                    },
                    new()
                    {
                        Id = "5",
                        Values = Helpers.EmbeddingValues(2),
                        Metadata = new Metadata { { "genre", "comedy" }, { "runtime", 90 } }
                    },
                    new()
                    {
                        Id = "6",
                        Values = Helpers.EmbeddingValues(2),
                        Metadata = new Metadata { { "genre", "romance" }, { "runtime", 240 } }
                    }
                },
                Namespace = targetNamespace
            }
        );

        // Upsert with dict
        await idx.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new() { Id = "7", Values = Helpers.EmbeddingValues(2) },
                    new() { Id = "8", Values = Helpers.EmbeddingValues(2) },
                    new() { Id = "9", Values = Helpers.EmbeddingValues(2) }
                },
                Namespace = targetNamespace
            }
        );

        if (wait)
            await PollFetchForIdsInNamespace(
                idx,
                ["1", "2", "3", "4", "5", "6", "7", "8", "9"],
                targetNamespace
            );
    }

    public static async Task SetupListData(IndexClient idx, string targetNamespace, bool wait)
    {
        // Upsert a bunch more stuff for testing list pagination
        for (var i = 0; i < 1000; i += 50)
        {
            var vectors = new List<Vector>();
            for (var d = 0; d < 50; d++)
                vectors.Add(
                    new Vector { Id = (i + d).ToString(), Values = Helpers.EmbeddingValues() }
                );

            await idx.UpsertAsync(
                new UpsertRequest { Vectors = vectors, Namespace = targetNamespace }
            );
        }

        if (wait)
            await PollFetchForIdsInNamespace(idx, ["999"], targetNamespace);
    }

    public static async Task PollFetchForIdsInNamespace(
        IndexClient idx,
        IEnumerable<string> ids,
        string namespaceName
    )
    {
        var maxSleep = int.TryParse(
            Environment.GetEnvironmentVariable("FRESHNESS_TIMEOUT_SECONDS"),
            out var timeout
        )
            ? timeout
            : 60;
        var deltaT = 5;
        var totalTime = 0;
        var done = false;

        while (!done)
        {
            Console.WriteLine(
                $"Attempting to fetch from \"{namespaceName}\". Total time waited: {totalTime} seconds"
            );
            var results = await idx.FetchAsync(
                new FetchRequest { Ids = ids.ToArray(), Namespace = namespaceName }
            );
            Console.WriteLine(results);

            var allPresent = ids.All(id => results.Vectors!.ContainsKey(id));
            if (allPresent)
            {
                done = true;
            }

            if (totalTime > maxSleep)
            {
                throw new TimeoutException(
                    $"Timed out waiting for namespace {namespaceName} to have vectors"
                );
            }

            totalTime += deltaT;
            await Task.Delay(deltaT * 1000); // Sleep for deltaT seconds
        }
    }
}
