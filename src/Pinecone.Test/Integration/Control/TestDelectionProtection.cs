using NUnit.Framework;
using Pinecone.Core;

namespace Pinecone.Test.Integration.Control;

public class TestDeletionProtection : BaseTest
{
    [Test]
    public async Task TestDeletionProtectionWorks()
    {
        var indexName = Helpers.GenerateIndexName("test-deletion-protection-works");
        // Create index with deletion protection enabled
        await Helpers
            .CreatePodIndexAndWaitUntilReady(
                Client,
                indexName,
                Environment,
                Dimension,
                Metric,
                true
            )
            .ConfigureAwait(false);

        var desc = await Client.DescribeIndexAsync(indexName).ConfigureAwait(false);
        TestContext.Out.WriteLine(desc.DeletionProtection);
        Assert.That(desc.DeletionProtection, Is.EqualTo(DeletionProtection.Enabled));

        // Attempt to delete the index should raise an exception
        var exceptionThrown = false;
        try
        {
            await Client.DeleteIndexAsync(indexName).ConfigureAwait(false);
        }
        catch (PineconeException)
        {
            exceptionThrown = true;
        }

        Assert.That(exceptionThrown, Is.True);

        // Disable deletion protection
        await Client
            .ConfigureIndexAsync(
                indexName,
                new ConfigureIndexRequest { DeletionProtection = DeletionProtection.Disabled }
            )
            .ConfigureAwait(false);
        desc = await Client.DescribeIndexAsync(indexName).ConfigureAwait(false);
        Assert.That(desc.DeletionProtection, Is.EqualTo(DeletionProtection.Disabled));

        // Now deletion should succeed
        await Helpers.TryDeleteIndex(Client, indexName).ConfigureAwait(false);
    }

    [Test]
    public async Task TestConfigureIndexWithDeletionProtection()
    {
        var indexName = Helpers.GenerateIndexName("configure-with-deletion-protection");
        // Create index with deletion protection enabled
        await Helpers
            .CreatePodIndexAndWaitUntilReady(
                Client,
                indexName,
                Environment,
                Dimension,
                Metric,
                true
            )
            .ConfigureAwait(false);

        var desc = await Client.DescribeIndexAsync(indexName).ConfigureAwait(false);
        Assert.That(desc.DeletionProtection, Is.EqualTo(DeletionProtection.Enabled));

        // Changing replicas only should not change deletion protection
        await Client
            .ConfigureIndexAsync(
                indexName,
                new ConfigureIndexRequest
                {
                    Spec = new ConfigureIndexRequestSpec
                    {
                        Pod = new ConfigureIndexRequestSpecPod { Replicas = 2 },
                    },
                }
            )
            .ConfigureAwait(false);
        desc = await Client.DescribeIndexAsync(indexName).ConfigureAwait(false);
        var replicaCount = desc.Spec.Match(_ => null, podSpec => podSpec.Pod?.Replicas);
        Assert.That(replicaCount, Is.EqualTo(2));
        Assert.That(desc.DeletionProtection, Is.EqualTo(DeletionProtection.Enabled));

        // Changing both replicas and deletion protection
        await Client
            .ConfigureIndexAsync(
                indexName,
                new ConfigureIndexRequest
                {
                    Spec = new ConfigureIndexRequestSpec
                    {
                        Pod = new ConfigureIndexRequestSpecPod { Replicas = 3 },
                    },
                    DeletionProtection = DeletionProtection.Disabled,
                }
            )
            .ConfigureAwait(false);
        desc = await Client.DescribeIndexAsync(indexName).ConfigureAwait(false);
        replicaCount = desc.Spec.Match(_ => null, podSpec => podSpec.Pod?.Replicas);
        Assert.That(replicaCount, Is.EqualTo(3));
        Assert.That(desc.DeletionProtection, Is.EqualTo(DeletionProtection.Disabled));
    }
}
