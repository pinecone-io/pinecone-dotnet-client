using NUnit.Framework;
using Pinecone.Inference;

namespace Pinecone.Test.Integration.Data;

[TestFixture]
public class TestModels : BaseTest
{
    [Test]
    public async Task TestListModels()
    {
        var models = await Client.Inference.Models.ListAsync(new ListModelsRequest()
        {
            Type = ModelType.Embed,
            VectorType = VectorType.Dense
        });
        await TestContext.Out.WriteLineAsync(models.Models.First().ToString());
        Assert.That(models, Is.Not.Null);
        Assert.That(models.Models, Is.Not.Null);
        Assert.That(models.Models, Is.Not.Empty);
    }


    [Test]
    public async Task TestGetModel()
    {
        const string modelName = "pinecone-sparse-english-v0";
        var model = await Client.Inference.Models.GetAsync(modelName);
        await TestContext.Out.WriteLineAsync(model.ToString());
        Assert.That(model, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(model.Type, Is.EqualTo(ModelType.Embed));
            Assert.That(model.VectorType!.Value, Is.EqualTo(VectorType.Sparse));
            Assert.That(model.Model, Is.EqualTo(modelName));
        });
    }
}