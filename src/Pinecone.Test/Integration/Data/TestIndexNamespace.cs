using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

[TestFixture]
public class TestIndexNamespace : BaseTest
{
    private string _indexName;
    private string _namespace;
    private IndexClient _client;

    [SetUp]
    public async Task SetUp()
    {
        _indexName = Helpers.GenerateIndexName("create-index-for-upsert-testing");
        _namespace = Helpers.RandomString(10);
        const string region = "us-east-1";
        const string model = "multilingual-e5-large";
        const CreateIndexForModelRequestCloud cloud = CreateIndexForModelRequestCloud.Aws;

        var index = await Client.CreateIndexForModelAsync(
            new CreateIndexForModelRequest
            {
                Name = _indexName,
                Cloud = cloud,
                Region = region,
                Embed = new CreateIndexForModelRequestEmbed
                {
                    Model = model,
                    Metric = CreateIndexForModelRequestEmbedMetric.Cosine,
                    FieldMap = new Dictionary<string, object?> { ["text"] = "chunk_text" },
                },
                DeletionProtection = DeletionProtection.Disabled,
            }
        );

        await Task.Delay(TimeSpan.FromSeconds(10));
        _client = Client.Index(index.Name, index.Host);
    }

    [TearDown]
    public async Task TearDown()
    {
        await Client.DeleteIndexAsync(_indexName);
    }

    [Test]
    public async Task ShouldUpsertAndFindRecordsInNamespace()
    {
        await _client.UpsertRecordsAsync(
            _namespace,
            [
                new UpsertRecord
                {
                    Id = "rec1",
                    AdditionalProperties =
                    {
                        ["chunk_text"] =
                            "Apples are a great source of dietary fiber, which supports digestion and helps maintain a healthy gut.",
                        ["category"] = "digestive system",
                    },
                },
                new UpsertRecord
                {
                    Id = "rec2",
                    AdditionalProperties =
                    {
                        ["chunk_text"] =
                            "Apples originated in Central Asia and have been cultivated for thousands of years, with over 7,500 varieties available today.",
                        ["category"] = "cultivation",
                    },
                },
                new UpsertRecord
                {
                    Id = "rec3",
                    AdditionalProperties =
                    {
                        ["chunk_text"] =
                            "Rich in vitamin C and other antioxidants, apples contribute to immune health and may reduce the risk of chronic diseases.",
                        ["category"] = "immune system",
                    },
                },
                new UpsertRecord
                {
                    Id = "rec4",
                    AdditionalProperties =
                    {
                        ["chunk_text"] =
                            "The high fiber content in apples can also help regulate blood sugar levels, making them a favorable snack for people with diabetes.",
                        ["category"] = "endocrine system",
                    },
                },
            ]
        );

        await Task.Delay(10_000);

        var response = await _client.SearchRecordsAsync(
            _namespace,
            new SearchRecordsRequest
            {
                Query = new SearchRecordsRequestQuery
                {
                    TopK = 4,
                    Inputs = new Dictionary<string, object?> { { "text", "Disease prevention" } },
                },
                Fields = ["category", "chunk_text"],
            }
        );

        Assert.That(response, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(response.Result, Is.Not.Null);
            Assert.That(response.Usage, Is.Not.Null);
            var hits = response.Result.Hits.ToList();
            Assert.That(hits, Has.Count.GreaterThan(0));
        });
    }
}
