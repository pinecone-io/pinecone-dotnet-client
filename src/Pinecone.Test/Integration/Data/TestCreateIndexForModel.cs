using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

public class TestCreateIndexForModel : BaseTest
{
    [Test]
    public async Task Should_Create_Index_For_Model()
    {
        var indexName = Helpers.GenerateIndexName("create-index-for-model-testing");
        const string region = "us-east-1";
        const string model = "multilingual-e5-large";
        const string tagKey = "foo";
        const string tagValue = "bar";
        const CreateIndexForModelRequestCloud cloud = CreateIndexForModelRequestCloud.Aws;
        var index = await Client
            .CreateIndexForModelAsync(
                new CreateIndexForModelRequest
                {
                    Name = indexName,
                    Cloud = cloud,
                    Region = region,
                    Embed = new CreateIndexForModelRequestEmbed
                    {
                        Model = model,
                        Metric = CreateIndexForModelRequestEmbedMetric.Cosine,
                        FieldMap = new Dictionary<string, object>()
                        {
                            { "text", "your-text-field" },
                        },
                    },
                    DeletionProtection = DeletionProtection.Disabled,
                    Tags = new Dictionary<string, string> { [tagKey] = tagValue },
                }
            )
            .ConfigureAwait(false);
        Assert.Multiple(() =>
        {
            Assert.That(index, Is.Not.Null);
            Assert.That(index.Name, Is.EqualTo(indexName));
            Assert.That(index.Embed?.Model, Is.EqualTo(model));
            Assert.That(index.DeletionProtection, Is.EqualTo(DeletionProtection.Disabled));
            Assert.That(index.Tags, Is.Not.Null);
            Assert.That(index.Tags, Has.Count.EqualTo(1));
            Assert.That(index.Tags, Does.ContainKey(tagKey));
            Assert.That(index.Tags?[tagKey], Is.EqualTo(tagValue));

            Assert.That(index.Spec.IsT0);
            var spec = index.Spec.AsT0;
            Assert.That(spec, Is.Not.Null);
            Assert.That(spec.Serverless.Cloud.ToString(), Is.EqualTo(cloud.ToString()));
            Assert.That(spec.Serverless.Region, Is.EqualTo(region));
        });
    }
}
