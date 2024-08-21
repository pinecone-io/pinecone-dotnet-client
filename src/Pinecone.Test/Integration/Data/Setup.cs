using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

[SetUpFixture]
public static class Setup
{
    public static string ApiKey { get; private set; } = null!;
    public static PineconeClient Client { get; private set; } = null!;
    public static string IndexHost { get; private set; } = null!;
    public static string IndexName { get; private set; } = null!;
    public static string ListNamespace { get; private set; } = null!;
    public static CreateIndexRequestMetric Metric { get; private set; }
    public static string Namespace { get; private set; } = null!;
    public static ServerlessIndexSpec Spec { get; private set; } = null!;
    public static IndexClient IndexClient { get; private set; } = null!;
    private static bool _isInitialized = false;
    private static readonly object Lock = new();

    public static void Initialize()
    {
        if (_isInitialized)
            return;

        lock (Lock)
        {
            if (_isInitialized)
                return;

            ApiKey = Helpers.GetEnvironmentVar("PINECONE_API_KEY");
            Client = new PineconeClient(ApiKey);
            Metric = CreateIndexRequestMetric.Cosine;
            Spec = new ServerlessIndexSpec
            {
                Serverless = new ServerlessSpec
                {
                    Cloud = ServerlessSpecCloud.Aws,
                    Region = "us-east-1"
                }
            };
            IndexName = "dataplane-" + Helpers.RandomString(20);
            Namespace = Helpers.RandomString(10);
            ListNamespace = Helpers.RandomString(10);

            IndexHost = Task.Run(() => SetupIndex(IndexName, Metric, Spec)).Result;
            IndexClient = Client.Index(host: IndexHost);
            Task.Run(SeedData).Wait();

            _isInitialized = true;
        }
    }

    private static async Task<string> SetupIndex(
        string indexName,
        CreateIndexRequestMetric metric,
        dynamic spec
    )
    {
        Console.WriteLine("Creating index with name: " + indexName);
        var indexes = await Client.ListIndexesAsync();
        if (indexes.Indexes != null && indexes.Indexes.All(index => index.Name != indexName))
            await Client.CreateIndexAsync(
                new CreateIndexRequest
                {
                    Name = indexName,
                    Dimension = 2,
                    Metric = metric,
                    Spec = spec
                }
            );

        var description = await Client.DescribeIndexAsync(indexName);
        return description.Host;
    }

    private static async Task SeedData()
    {
        Console.WriteLine("Sleeping while index boots up...");

        Thread.Sleep(10000);
        Console.WriteLine("Seeding data in host " + IndexHost);

        Console.WriteLine("Seeding list data in namespace " + ListNamespace);
        await Seed.SetupListData(IndexClient, ListNamespace, true);

        Console.WriteLine("Seeding data in namespace " + Namespace);
        await Seed.SetupData(IndexClient, Namespace, true);

        Console.WriteLine("Seeding data in namespace \"\"");
        await Seed.SetupData(IndexClient, "", true);

        Console.WriteLine("Waiting a bit more to ensure freshness");
    }

    [OneTimeTearDown]
    public static async Task GlobalCleanup()
    {
        await Helpers.Cleanup(Client);
    }
}
