using System.Text.RegularExpressions;

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

    public static string GenerateIndexName(string testName)
    {
        var buildNumber = Environment.GetEnvironmentVariable("GITHUB_BUILD_NUMBER");

        if (testName.StartsWith("test_", StringComparison.OrdinalIgnoreCase))
            testName = testName.Substring(5);

        // Trim name length to save space for other info in name
        testName = testName.Length > 20 ? testName.Substring(0, 20) : testName;

        // Remove trailing underscore, if any
        if (testName.EndsWith("_"))
            testName = testName.Substring(0, testName.Length - 1);

        var randomString = RandomString(45);
        var nameParts = new[] { buildNumber, testName, randomString };
        var indexName = string.Join("-", nameParts.Where(x => x != null));

        // Remove invalid characters
        indexName = Regex.Replace(indexName, @"[\[\(_,\s]", "-");
        indexName = Regex.Replace(indexName, @"[\]\)\.]", "");

        const int maxLength = 45;
        indexName = indexName.Length > maxLength ? indexName.Substring(0, maxLength) : indexName;

        // Trim final character if it is not alphanumeric
        if (indexName.EndsWith("_") || indexName.EndsWith("-"))
            indexName = indexName.Substring(0, indexName.Length - 1);

        return indexName.ToLower();
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
        var maxSleep = int.TryParse(
            Environment.GetEnvironmentVariable("FRESHNESS_TIMEOUT_SECONDS"),
            out var timeout
        )
            ? timeout
            : 60;
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
                stats.Namespaces.ContainsKey(namespaceName)
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

    public static void PollFetchForIdsInNamespace(
        IndexClient idx,
        string[] ids,
        string namespaceName
    )
    {
        var maxSleep = int.TryParse(
            Environment.GetEnvironmentVariable("FRESHNESS_TIMEOUT_SECONDS"),
            out var timeout
        )
            ? timeout
            : 60;
        const int deltaT = 5;
        var totalTime = 0;
        var done = false;

        while (!done)
        {
            Console.WriteLine(
                $"Attempting to fetch from \"{namespaceName}\". Total time waited: {totalTime} seconds"
            );

            var results = idx.FetchAsync(
                new FetchRequest { Ids = ids, Namespace = namespaceName }
            ).Result;

            Console.WriteLine(results);

            var allPresent = ids.All(id => results.Vectors.ContainsKey(id));
            if (allPresent)
                done = true;

            if (totalTime > maxSleep)
                throw new TimeoutException(
                    $"Timed out waiting for namespace {namespaceName} to have vectors"
                );

            totalTime += deltaT;
            Thread.Sleep(deltaT * 1000);
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
}
