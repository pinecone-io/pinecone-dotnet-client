using NUnit.Framework;

namespace Pinecone.Test.Integration;

public static class Helpers
{
    public static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        var random = new Random();
        return new string(
            Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray()
        );
    }

    public static async Task CreateCollectionAndWaitUntilReady(
        PineconeClient client,
        string collectionName,
        string indexName
    )
    {
        await client.CreateCollectionAsync(
            new CreateCollectionRequest { Name = collectionName, Source = indexName }
        ).ConfigureAwait(false);

        var timeWaited = 0;
        var desc = await client.DescribeCollectionAsync(collectionName).ConfigureAwait(false);
        var collectionReady = desc.Status;
        while (collectionReady != CollectionModelStatus.Ready && timeWaited < 180)
        {
            await TestContext.Out.WriteLineAsync(
                $"Waiting for collection {collectionName} to be ready. Waited {timeWaited} seconds..."
            ).ConfigureAwait(false);
            await Task.Delay(5000).ConfigureAwait(false);
            timeWaited += 5;
            desc = await client.DescribeCollectionAsync(collectionName).ConfigureAwait(false);
            collectionReady = desc.Status;
        }
        if (timeWaited >= 180)
        {
            throw new Exception($"Collection {collectionName} is not ready after 180 seconds");
        }
        // extra wait to ensure true readiness
        await Task.Delay(20000).ConfigureAwait(false);
    }

    public static async Task<string> CreatePodIndexAndWaitUntilReady(
        PineconeClient client,
        string indexName,
        string environment,
        int dimension,
        CreateIndexRequestMetric metric,
        bool deletionProtection = false,
        string? sourceCollection = null
    )
    {
        var index = await client.CreateIndexAsync(
            CreateIndexParams(
                indexName,
                environment,
                dimension,
                metric,
                deletionProtection,
                sourceCollection
            )
        ).ConfigureAwait(false);
        var indexReady = false;
        var timeWaited = 0;
        while (!indexReady && timeWaited < 120)
        {
            await TestContext.Out.WriteLineAsync(
                $"Waiting for index {indexName} to be ready. Waited {timeWaited} seconds..."
            ).ConfigureAwait(false);
            await Task.Delay(5000).ConfigureAwait(false);
            timeWaited += 5;
            try
            {
                var status = (await client.DescribeIndexAsync(indexName).ConfigureAwait(false)).Status;
                indexReady = status.Ready;
            }
            catch (NotFoundError)
            {
                await TestContext.Out.WriteLineAsync("Index not found yet.").ConfigureAwait(false);
            }
        }
        await TestContext.Out.WriteLineAsync($"Index {indexName} has a {indexReady} ready status!").ConfigureAwait(false);
        if (timeWaited > 120)
        {
            throw new Exception($"Index {indexName} is not ready after 120 seconds");
        }
        // extra wait to ensure true readiness
        await Task.Delay(120000).ConfigureAwait(false);
        return index.Host;
    }

    private static CreateIndexRequest CreateIndexParams(
        string indexName,
        string environment,
        int dimension,
        CreateIndexRequestMetric metric,
        bool deletionProtection = false,
        string? sourceCollection = null
    )
    {
        return new CreateIndexRequest
        {
            Name = indexName,
            Dimension = dimension,
            Metric = metric,
            Spec = new PodIndexSpec
            {
                Pod = new PodSpec
                {
                    Environment = environment,
                    PodType = "p1.x1",
                    Replicas = 1,
                    Shards = 1,
                    Pods = 1,
                    SourceCollection = sourceCollection
                }
            },
            DeletionProtection = deletionProtection
                ? DeletionProtection.Enabled
                : DeletionProtection.Disabled
        };
    }

    public static string GetEnvironmentVar(string name, string? defaultVal = null)
    {
        return Environment.GetEnvironmentVariable(name)
            ?? defaultVal
            ?? throw new Exception($"Expected environment variable {name} is not set");
    }

    public static async Task PollStatsForNamespaceAsync(
        IndexClient idx,
        string namespaceName,
        int expectedCount
    )
    {
        const int maxSleep = 120;
        const int deltaT = 1;
        var totalTime = 0;
        var done = false;

        while (!done)
        {
            await TestContext.Out.WriteLineAsync(
                $"Waiting for namespace \"{namespaceName}\" to have vectors. Total time waited: {totalTime} seconds"
            ).ConfigureAwait(false);

            var stats = await idx.DescribeIndexStatsAsync(new DescribeIndexStatsRequest()).ConfigureAwait(false);

            if (
                stats.Namespaces!.ContainsKey(namespaceName)
                && stats.Namespaces[namespaceName].VectorCount >= expectedCount
            )
            {
                done = true;
            }
            else if (totalTime > maxSleep)
            {
                throw new TimeoutException(
                    $"Timed out waiting for namespace {namespaceName} to have vectors"
                );
            }
            else
            {
                totalTime += deltaT;
                await Task.Delay(deltaT * 1000).ConfigureAwait(false);
            }
        }
    }

    public static float[] EmbeddingValues(int dimension = 2)
    {
        var random = new Random();
        var values = new float[dimension];
        for (var i = 0; i < dimension; i++)
        {
            values[i] = (float)random.NextDouble();
        }

        return values;
    }

    public static string GenerateIndexName(string testName)
    {
        return $"{testName}-{RandomString(10)}";
    }

    public static string FakeApiKey()
    {
        return string.Join(
            "-",
            RandomString(8),
            RandomString(4),
            RandomString(4),
            RandomString(4),
            RandomString(12)
        );
    }

    public static async Task<bool> IndexExistsAsync(PineconeClient client, string indexName)
    {
        var indexes = await client.ListIndexesAsync().ConfigureAwait(false);
        return indexes.Indexes!.Any(index => index.Name == indexName);
    }

    public static async Task TryDeleteIndex(PineconeClient client, string indexName)
    {
        var index = await client.DescribeIndexAsync(indexName).ConfigureAwait(false);
        await TryDeleteIndex(client, index).ConfigureAwait(false);
    }

    public static async Task TryDeleteIndex(PineconeClient client, Index index)
    {
        var indexName = index.Name;
        var timeWaited = 0;
        while (await IndexExistsAsync(client, indexName).ConfigureAwait(false) && timeWaited < 120)
        {
            // Turn off deletion protection in case it's on
            if (index.DeletionProtection == DeletionProtection.Enabled)
            {
                try
                {
                    await TestContext.Out.WriteLineAsync(
                        $"Attempting turn off deletion protection of index {indexName}"
                    ).ConfigureAwait(false);
                    await TurnOffDelectionProtection(client, index).ConfigureAwait(false);
                    await TestContext.Out.WriteLineAsync($"Turned off deletion protection of index {indexName}").ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    await TestContext.Out.WriteLineAsync(
                        $"Unable to turn of deletion protection of index {indexName}: {e.Message}"
                    ).ConfigureAwait(false);
                    timeWaited += 5;
                    await Task.Delay(5000).ConfigureAwait(false);
                    continue;
                }
            }
            try
            {
                await TestContext.Out.WriteLineAsync($"Attempting delete of index {indexName}").ConfigureAwait(false);
                await client.DeleteIndexAsync(indexName).ConfigureAwait(false);
                await TestContext.Out.WriteLineAsync($"Deleted index {indexName}").ConfigureAwait(false);
                break;
            }
            catch (Exception e)
            {
                await TestContext.Out.WriteLineAsync($"Unable to delete index {indexName}: {e.Message}").ConfigureAwait(false);
            }
            await TestContext.Out.WriteLineAsync(
                $"Waiting for index {indexName} to be ready to delete. Waited {timeWaited} seconds..."
            ).ConfigureAwait(false);
            timeWaited += 5;
            await Task.Delay(5000).ConfigureAwait(false);
        }

        if (timeWaited >= 120)
        {
            throw new Exception($"Index {indexName} could not be deleted after 120 seconds");
        }
    }

    public static async Task TurnOffDelectionProtection(PineconeClient client, Index index)
    {
        await client.ConfigureIndexAsync(
            index.Name,
            new ConfigureIndexRequest { DeletionProtection = DeletionProtection.Disabled }
        ).ConfigureAwait(false);
    }

    public static async Task TryDeleteCollection(PineconeClient client, string collectionName)
    {
        var timeWaited = 0;
        while (timeWaited < 120)
        {
            await TestContext.Out.WriteLineAsync(
                $"Waiting for collection {collectionName} to be ready to delete. Waited {timeWaited} seconds..."
            ).ConfigureAwait(false);
            timeWaited += 5;
            await Task.Delay(5000).ConfigureAwait(false);
            try
            {
                await TestContext.Out.WriteLineAsync($"Attempting delete of collection {collectionName}").ConfigureAwait(false);
                await client.DeleteCollectionAsync(collectionName).ConfigureAwait(false);
                await TestContext.Out.WriteLineAsync($"Deleted collection {collectionName}").ConfigureAwait(false);
                break;
            }
            catch (Exception e)
            {
                await TestContext.Out.WriteLineAsync($"Unable to delete collection {collectionName}: {e.Message}").ConfigureAwait(false);
            }
        }

        if (timeWaited >= 120)
        {
            throw new Exception($"Index {collectionName} could not be deleted after 120 seconds");
        }
    }

    public static async Task Cleanup(PineconeClient client)
    {
        var indexes = await client.ListIndexesAsync().ConfigureAwait(false);
        foreach (var index in indexes.Indexes ?? [])
        {
            await TryDeleteIndex(client, index).ConfigureAwait(false);
        }
        var collections = await client.ListCollectionsAsync().ConfigureAwait(false);
        foreach (var collection in collections.Collections ?? [])
        {
            await TryDeleteCollection(client, collection.Name).ConfigureAwait(false);
        }
    }

    private static string GetEnvironmentVariable(string name)
    {
        return Environment.GetEnvironmentVariable(name)
            ?? throw new Exception($"Expected environment variable {name} is not set");
    }
}
