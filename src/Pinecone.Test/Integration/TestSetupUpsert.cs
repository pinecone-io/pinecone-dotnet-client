using NUnit.Framework;

namespace Pinecone.Test.Integration;

public class TestSetupUpsert : BaseDataPlaneTest
{
    [TestCase(true)]
    [TestCase(false)]
    public async Task TestUpsertToNamespace(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? _namespace : "";

        // Upsert with tuples
        await _indexClient.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new() { Id = "1", Values = Helpers.EmbeddingValues() },
                    new() { Id = "2", Values = Helpers.EmbeddingValues() },
                    new() { Id = "3", Values = Helpers.EmbeddingValues() }
                },
                Namespace = targetNamespace
            }
        );

        // Upsert with objects
        await _indexClient.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new() { Id = "4", Values = Helpers.EmbeddingValues() },
                    new() { Id = "5", Values = Helpers.EmbeddingValues() },
                    new() { Id = "6", Values = Helpers.EmbeddingValues() }
                },
                Namespace = targetNamespace
            }
        );

        // Upsert with dictionary
        await _indexClient.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new() { Id = "7", Values = Helpers.EmbeddingValues() },
                    new() { Id = "8", Values = Helpers.EmbeddingValues() },
                    new() { Id = "9", Values = Helpers.EmbeddingValues() }
                },
                Namespace = targetNamespace
            }
        );

        Helpers.PollStatsForNamespace(_indexClient, targetNamespace, 9);

        // Check the vector count reflects that some data has been upserted
        var stats = await _indexClient.DescribeIndexStatsAsync(new DescribeIndexStatsRequest());
        Assert.That(stats.TotalVectorCount, Is.GreaterThanOrEqualTo(9));
        Assert.That(stats.Namespaces![targetNamespace].VectorCount, Is.EqualTo(9));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestUpsertToNamespaceWithSparseEmbeddingValues(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? _namespace : "";

        // Upsert with sparse values object
        await _indexClient.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new()
                    {
                        Id = "1",
                        Values = Helpers.EmbeddingValues(),
                        SparseValues = new SparseValues
                        {
                            Indices = new List<uint> { 0, 1 },
                            Values = Helpers.EmbeddingValues()
                        }
                    }
                },
                Namespace = targetNamespace
            }
        );

        // Upsert with sparse values dictionary
        await _indexClient.UpsertAsync(
            new UpsertRequest
            {
                Vectors = new List<Vector>
                {
                    new()
                    {
                        Id = "2",
                        Values = Helpers.EmbeddingValues(),
                        SparseValues = new SparseValues
                        {
                            Indices = new List<uint> { 0, 1 },
                            Values = Helpers.EmbeddingValues()
                        }
                    },
                    new Vector
                    {
                        Id = "3",
                        Values = Helpers.EmbeddingValues(),
                        SparseValues = new SparseValues
                        {
                            Indices = new List<uint> { 0, 1 },
                            Values = Helpers.EmbeddingValues()
                        }
                    }
                },
                Namespace = targetNamespace
            }
        );

        Helpers.PollStatsForNamespace(_indexClient, targetNamespace, 9);

        // Check the vector count reflects that some data has been upserted
        var stats = await _indexClient.DescribeIndexStatsAsync(new DescribeIndexStatsRequest());
        Assert.That(stats.TotalVectorCount, Is.GreaterThanOrEqualTo(9));
        Assert.That(stats.Namespaces![targetNamespace].VectorCount, Is.EqualTo(9));
    }
}
