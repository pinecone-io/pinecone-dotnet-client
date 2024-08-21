using NUnit.Framework;

namespace Pinecone.Test.Integration.Control;

public class TestConfigurePodIndex : BaseTest
{
    [Test]
    public async Task TestConfigurePodIndexSucceeds()
    {
        var indexName = Helpers.GenerateIndexName("test-configure-pod-index-succeeds");
        await Helpers.CreatePodIndexAndWaitUntilReady(
            Client,
            indexName,
            Environment,
            Dimension,
            Metric
        );
        await Client.ConfigureIndexAsync(
            indexName,
            new ConfigureIndexRequest
            {
                Spec = new ConfigureIndexRequestSpec
                {
                    Pod = new ConfigureIndexRequestSpecPod { Replicas = 1, PodType = "p1.x1" }
                }
            }
        );
    }
}
