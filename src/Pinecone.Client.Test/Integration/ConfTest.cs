using Newtonsoft.Json;
using NUnit.Framework;

namespace Pinecone.Client.Test.Integration;

public class ConfTest
{
    private string _apiKey;
    private Pinecone _client;
    private string _indexHost;
    private string _indexName;
    private string _listNamespace;
    private string _metric;
    private string _namespace;
    private dynamic _spec;
    private string _weirdIdsNamespace;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        _apiKey = Helpers.GetEnvironmentVar("");
        _client = new Pinecone(_apiKey);
        _metric = Utils.GetEnvironmentVar("METRIC", "cosine");
        _spec = JsonConvert.DeserializeObject<dynamic>(Utils.GetEnvironmentVar("SPEC"));
        _indexName = "dataplane-" + Helpers.RandomString(20);
        _namespace = Helpers.RandomString(10);
        _listNamespace = Helpers.RandomString(10);
        _weirdIdsNamespace = Helpers.RandomString(10);

        _indexHost = await SetupIndex(_client, _indexName, _metric, _spec);

        await SeedData();
    }

    private async Task<string> SetupIndex(IndexClient client, string indexName, string metric, dynamic spec)
    {
        Console.WriteLine("Creating index with name: " + indexName);
        var pc = Helpers.BuildClient();

        var indexes = await pc.ListIndexesAsync();
        if (!indexes.Names.Contains(indexName))
            await pc.CreateIndexAsync(new CreateIndexRequest
            {
                Name = indexName,
                Dimension = 2,
                Metric = metric,
                Spec = spec
            });

        var description = await pc.DescribeIndexAsync(new DescribeIndexRequest { Name = indexName });
        return description.Host;
    }

    private async Task SeedData()
    {
        Console.WriteLine("Seeding data in host " + _indexHost);

        // Console.WriteLine("Seeding data in weird ids namespace " + _weirdIdsNamespace);
        // await Utils.SetupWeirdIdsData(_client, _weirdIdsNamespace, true);

        Console.WriteLine("Seeding list data in namespace " + _listNamespace);
        await Seed.SetupListData(_client.Index(host: _indexHost), _listNamespace, true);

        Console.WriteLine("Seeding data in namespace " + _namespace);
        await Seed.SetupData(_client, _namespace, false);

        Console.WriteLine("Seeding data in namespace \"\"");
        await Seed.SetupData(_client, "", true);

        Console.WriteLine("Waiting a bit more to ensure freshness");
        await Task.Delay(120000); // 120 seconds
    }

    [OneTimeTearDown]
    public async Task GlobalTeardown()
    {
        Console.WriteLine("Deleting index with name: " + _indexName);
        await _client.DeleteIndexAsync(_indexName);
    }

    [Test]
    public void ExampleTest()
    {
        // Example test using seeded data
        Assert.IsTrue(true); // Replace with actual tests
    }
}