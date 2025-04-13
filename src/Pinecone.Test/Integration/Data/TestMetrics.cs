/*using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

public class TestMetrics : BaseTest
{
    [Test]
    public async Task Should_Return_PrometheusTargets()
    {
        const string projectId = "ea8b4d7f-1799-4588-9545-63b019e46e68";
        var targetItems = (await Client.Metrics.FetchPrometheusTargetsAsync(projectId))?.ToArray();
        Assert.That(targetItems, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(targetItems, Is.InstanceOf<PrometheusTargetsItem[]>());
            Assert.That(targetItems, Is.Not.Empty);
            
            var targets = targetItems[0].Targets?.ToArray();
            Assert.That(targets, Is.Not.Null);
            Assert.That(targets, Is.Not.Empty);
            Assert.That(targets?[0], Is.Not.Null);
            
            var labels = targetItems[0].Labels;
            Assert.That(labels, Is.Not.Null);
            Assert.That(labels, Is.Not.Empty);
            var label = labels?.First();
            Assert.That(label, Is.Not.Null);
            Assert.That(label?.Key, Is.Not.Null);
            Assert.That(label?.Key, Is.Not.Empty);
            Assert.That(label?.Value, Is.Not.Null);
            Assert.That(label?.Value, Is.Not.Empty);
        });
    }
    
    [Test]
    public void Should_Not_Find_PrometheusTargets()
    {
        const string projectId = "";
        Assert.ThrowsAsync<PineconeApiException>(async () => await Client.Metrics.FetchPrometheusTargetsAsync(projectId));
    }
}*/
