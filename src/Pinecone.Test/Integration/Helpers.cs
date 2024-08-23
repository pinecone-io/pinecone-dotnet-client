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
        );

        var timeWaited = 0;
        var desc = await client.DescribeCollectionAsync(collectionName);
        var collectionReady = desc.Status;
        while (collectionReady != CollectionModelStatus.Ready && timeWaited < 180)
        {
            Console.WriteLine(
                $"Waiting for collection {collectionName} to be ready. Waited {timeWaited} seconds..."
            );
            await Task.Delay(5000);
            timeWaited += 5;
            desc = await client.DescribeCollectionAsync(collectionName);
            collectionReady = desc.Status;
        }
        if (timeWaited >= 180)
        {
            throw new Exception($"Collection {collectionName} is not ready after 180 seconds");
        }
        // extra wait to ensure true readiness
        await Task.Delay(20000);
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
        );
        var indexReady = false;
        var timeWaited = 0;
        while (!indexReady && timeWaited < 120)
        {
            Console.WriteLine(
                $"Waiting for index {indexName} to be ready. Waited {timeWaited} seconds..."
            );
            await Task.Delay(5000);
            timeWaited += 5;
            try
            {
                var status = (await client.DescribeIndexAsync(indexName)).Status;
                indexReady = status.Ready;
            }
            catch (NotFoundError)
            {
                Console.WriteLine(
                    "Index not found yet."
                );
            }
        }
        if (timeWaited > 120)
        {
            throw new Exception($"Index {indexName} is not ready after 120 seconds");
        }
        // extra wait to ensure true readiness
        await Task.Delay(30000);
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

    public static void PollStatsForNamespace(
        IndexClient idx,
        string namespaceName,
        int expectedCount
    )
    {
        var maxSleep = 120;
        const int deltaT = 5;
        var totalTime = 0;
        var done = false;

        while (!done)
        {
            Console.WriteLine(
                $"Waiting for namespace \"{namespaceName}\" to have vectors. Total time waited: {totalTime} seconds"
            );

            var stats = idx.DescribeIndexStatsAsync(new DescribeIndexStatsRequest()).Result;

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
                Thread.Sleep(deltaT * 1000);
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
        var indexes = await client.ListIndexesAsync();
        return indexes.Indexes!.Any(index => index.Name == indexName);
    }

    public static async Task TryDeleteIndex(PineconeClient client, string indexName)
    {
        var index = await client.DescribeIndexAsync(indexName);
        await TryDeleteIndex(client, index);
    }

    public static async Task TryDeleteIndex(PineconeClient client, Index index)
    {
        var indexName = index.Name;
        var timeWaited = 0;
        while (await IndexExistsAsync(client, indexName) && timeWaited < 120)
        {
            // Turn off deletion protection in case it's on
            if (index.DeletionProtection == DeletionProtection.Enabled)
            {
                try
                {
                    Console.WriteLine(
                        $"Attempting turn off deletion protection of index {indexName}"
                    );
                    await TurnOffDelectionProtection(client, index);
                    Console.WriteLine($"Turned off deletion protection of index {indexName}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        $"Unable to turn of deletion protection of index {indexName}: {e.Message}"
                    );
                    timeWaited += 5;
                    await Task.Delay(5000);
                    continue;
                }
            }
            try
            {
                Console.WriteLine($"Attempting delete of index {indexName}");
                await client.DeleteIndexAsync(indexName);
                Console.WriteLine($"Deleted index {indexName}");
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to delete index {indexName}: {e.Message}");
            }
            Console.WriteLine(
                $"Waiting for index {indexName} to be ready to delete. Waited {timeWaited} seconds..."
            );
            timeWaited += 5;
            await Task.Delay(5000);
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
        );
    }

    public static async Task TryDeleteCollection(PineconeClient client, string collectionName)
    {
        var timeWaited = 0;
        while (timeWaited < 120)
        {
            Console.WriteLine(
                $"Waiting for collection {collectionName} to be ready to delete. Waited {timeWaited} seconds..."
            );
            timeWaited += 5;
            await Task.Delay(5000);
            try
            {
                Console.WriteLine($"Attempting delete of collection {collectionName}");
                await client.DeleteCollectionAsync(collectionName);
                Console.WriteLine($"Deleted collection {collectionName}");
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to delete collection {collectionName}: {e.Message}");
            }
        }

        if (timeWaited >= 120)
        {
            throw new Exception($"Index {collectionName} could not be deleted after 120 seconds");
        }
    }

    public static async Task Cleanup(PineconeClient client)
    {
        var indexes = await client.ListIndexesAsync();
        foreach (var index in indexes.Indexes ?? [])
        {
            await TryDeleteIndex(client, index);
        }
        var collections = await client.ListCollectionsAsync();
        foreach (var collection in collections.Collections ?? [])
        {
            await TryDeleteCollection(client, collection.Name);
        }
    }

    private static string GetEnvironmentVariable(string name)
    {
        return Environment.GetEnvironmentVariable(name)
            ?? throw new Exception($"Expected environment variable {name} is not set");
    }
}
