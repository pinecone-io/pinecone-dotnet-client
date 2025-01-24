using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

[SetUpFixture]
[Parallelizable(ParallelScope.Children)]
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
        await TestContext.Out.WriteLineAsync("Initializing data plane integration tests...");
        Client = new PineconeClient(
            apiKey: Helpers.GetEnvironmentVar("PINECONE_API_KEY"),
            new ClientOptions { SourceTag = "test-tag" }
        );
        Metric = CreateIndexRequestMetric.Cosine;
        Spec = new ServerlessIndexSpec
        {
            Serverless = new ServerlessSpec
            {
                Cloud = ServerlessSpecCloud.Aws,
                Region = "us-east-1",
            },
        };
        IndexName = "dataplane-" + Helpers.RandomString(20);
        Namespace = Helpers.RandomString(10);
        ListNamespace = Helpers.RandomString(10);

        IndexHost = await SetupIndex(IndexName, Metric, Spec);
        IndexClient = Client.Index(host: IndexHost);
        await SeedData();
        await TestContext.Out.WriteLineAsync("Data plane integration test initialization complete.");
    }

    [OneTimeTearDown]
    public async Task GlobalCleanup()
    {
        await Helpers.Cleanup(Client);
    }

    private static async Task<string> SetupIndex(
        string indexName,
        CreateIndexRequestMetric metric,
        ServerlessIndexSpec spec
    )
    {
        await TestContext.Out.WriteLineAsync("Creating index with name: " + indexName);
        await Client.CreateIndexAsync(
            new CreateIndexRequest
            {
                Name = indexName,
                Dimension = 2,
                Metric = metric,
                Spec = spec,
            }
        );

        var description = await Client.DescribeIndexAsync(indexName);
        return description.Host;
    }

    private static async Task SeedData()
    {
        await TestContext.Out.WriteLineAsync("Sleeping while index boots up...");
        await Task.Delay(10_000);
        
        await TestContext.Out.WriteLineAsync("Seeding data in host " + IndexHost);

        await TestContext.Out.WriteLineAsync("Seeding list data in namespace " + ListNamespace);
        await Seed.SetupListData(IndexClient, ListNamespace, true);

        await TestContext.Out.WriteLineAsync("Seeding data in namespace " + Namespace);
        await Seed.SetupData(IndexClient, Namespace, true);

        await TestContext.Out.WriteLineAsync("Seeding data in namespace \"\"");
        await Seed.SetupData(IndexClient, "", true);
    }
}
