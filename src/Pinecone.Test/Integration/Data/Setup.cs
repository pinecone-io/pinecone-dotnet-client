using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

[SetUpFixture]
public class Setup
{
    public static PineconeClient Client { get; private set; } = null!;
    public static string IndexHost { get; private set; } = null!;
    public static string IndexName { get; private set; } = null!;
    public static string ListNamespace { get; private set; } = null!;
    public static CreateIndexRequestMetric Metric { get; private set; }
    public static string Namespace { get; private set; } = null!;
    public static ServerlessIndexSpec Spec { get; private set; } = null!;
    public static IndexClient IndexClient { get; private set; } = null!;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        Console.WriteLine("Initializing data plane integration tests...");
        Client = new PineconeClient(apiKey: Helpers.GetEnvironmentVar("PINECONE_API_KEY"));
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

        IndexHost = await SetupIndex(IndexName, Metric, Spec);
        IndexClient = Client.Index(host: IndexHost);
        await SeedData();
        Console.WriteLine("Data plane integration test initialization complete.");
    }

    [OneTimeTearDown]
    public async Task GlobalCleanup()
    {
        await Helpers.Cleanup(Client);
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
    }
}
