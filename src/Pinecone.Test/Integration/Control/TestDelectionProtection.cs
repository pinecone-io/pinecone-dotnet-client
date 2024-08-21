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
        await Helpers.CreatePodIndexAndWaitUntilReady(
            Client,
            indexName,
            Environment,
            Dimension,
            Metric,
            true
        );

        var desc = await Client.DescribeIndexAsync(indexName);
        Console.WriteLine(desc.DeletionProtection);
        Assert.That(desc.DeletionProtection, Is.EqualTo(DeletionProtection.Enabled));

        // Attempt to delete the index should raise an exception
        var exceptionThrown = false;
        try
        {
            await Client.DeleteIndexAsync(indexName);
        }
        catch (PineconeException)
        {
            exceptionThrown = true;
        }

        Assert.IsTrue(exceptionThrown);

        // Disable deletion protection
        await Client.ConfigureIndexAsync(
            indexName,
            new ConfigureIndexRequest { DeletionProtection = DeletionProtection.Disabled }
        );
        desc = await Client.DescribeIndexAsync(indexName);
        Assert.That(desc.DeletionProtection, Is.EqualTo(DeletionProtection.Disabled));

        // Now deletion should succeed
        await Helpers.TryDeleteIndex(Client, indexName: indexName);
    }

    [Test]
    public async Task TestConfigureIndexWithDeletionProtection()
    {
        var indexName = Helpers.GenerateIndexName("configure-with-deletion-protection");
        // Create index with deletion protection enabled
        await Helpers.CreatePodIndexAndWaitUntilReady(
            Client,
            indexName,
            Environment,
            Dimension,
            Metric,
            true
        );

        var desc = await Client.DescribeIndexAsync(indexName);
        Assert.That(desc.DeletionProtection, Is.EqualTo(DeletionProtection.Enabled));

        // Changing replicas only should not change deletion protection
        await Client.ConfigureIndexAsync(
            indexName,
            new ConfigureIndexRequest
            {
                Spec = new ConfigureIndexRequestSpec
                {
                    Pod = new ConfigureIndexRequestSpecPod { Replicas = 2 }
                }
            }
        );
        desc = await Client.DescribeIndexAsync(indexName);
        Assert.That(desc.Spec.Pod!.Replicas, Is.EqualTo(2));
        Assert.That(desc.DeletionProtection, Is.EqualTo(DeletionProtection.Enabled));

        // Changing both replicas and deletion protection
        await Client.ConfigureIndexAsync(
            indexName,
            new ConfigureIndexRequest
            {
                Spec = new ConfigureIndexRequestSpec
                {
                    Pod = new ConfigureIndexRequestSpecPod { Replicas = 3, }
                },
                DeletionProtection = DeletionProtection.Disabled
            }
        );
        desc = await Client.DescribeIndexAsync(indexName);
        Assert.That(desc.Spec.Pod!.Replicas, Is.EqualTo(3));
        Assert.That(desc.DeletionProtection, Is.EqualTo(DeletionProtection.Disabled));
    }
}
