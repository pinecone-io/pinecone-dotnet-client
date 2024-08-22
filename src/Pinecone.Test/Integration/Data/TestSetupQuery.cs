using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

public class TestSetupQuery : BaseTest
{
    private const int ExpectedDimension = 2;

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryById(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var results = await IndexClient.QueryAsync(
            new QueryRequest
            {
                Id = "1",
                Namespace = targetNamespace,
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));

        Assert.IsNotNull(results.Usage);
        Assert.IsNotNull(results.Usage.ReadUnits);
        Assert.Greater(results.Usage.ReadUnits, 0);

        // By default, does not include values or metadata
        var recordWithMetadata = results.Matches!.FirstOrDefault(match => match.Id == "4");
        Assert.IsNotNull(recordWithMetadata);
        Assert.IsNull(recordWithMetadata.Metadata);
        Assert.That(recordWithMetadata.Values?.IsEmpty, Is.True);
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByVector(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var results = await IndexClient.QueryAsync(
            new QueryRequest
            {
                Vector = EmbeddingValues(2),
                Namespace = targetNamespace,
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByVectorIncludeValues(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var results = await IndexClient.QueryAsync(
            new QueryRequest
            {
                Vector = EmbeddingValues(2),
                Namespace = targetNamespace,
                IncludeValues = true,
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.IsNotEmpty(results.Matches!);
        Assert.IsNotNull(results.Matches!.First().Values);
        Assert.That(results.Matches!.First().Values!.Value.Length, Is.EqualTo(ExpectedDimension));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByVectorIncludeMetadata(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var results = await IndexClient.QueryAsync(
            new QueryRequest
            {
                Vector = EmbeddingValues(2),
                Namespace = targetNamespace,
                IncludeMetadata = true,
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));

        var matchesWithMetadata = results.Matches!.Where(match => match.Metadata != null).ToList();
        Assert.That(matchesWithMetadata.Count, Is.EqualTo(3));
        var matchWithGenre = matchesWithMetadata.FirstOrDefault(match => match.Id == "4");
        Assert.IsNotNull(matchWithGenre);
        Assert.That(matchWithGenre.Metadata!["genre"], Is.EqualTo(new MetadataValue("action")));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByVectorIncludeValuesAndMetadata(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var results = await IndexClient.QueryAsync(
            new QueryRequest
            {
                Vector = EmbeddingValues(2),
                Namespace = targetNamespace,
                IncludeValues = true,
                IncludeMetadata = true,
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));

        var matchesWithMetadata = results.Matches!.Where(match => match.Metadata != null).ToList();
        Assert.That(matchesWithMetadata.Count, Is.EqualTo(3));
        var matchWithGenre = matchesWithMetadata.FirstOrDefault(match => match.Id == "4");
        Assert.IsNotNull(matchWithGenre);
        Assert.That(matchWithGenre.Metadata!["genre"], Is.EqualTo(new MetadataValue("action")));
        Assert.That(results.Matches!.First().Values!.Value.Length, Is.EqualTo(ExpectedDimension));
    }

    // Helper method to generate embedding values (float array) of a given dimension
    private float[] EmbeddingValues(int dimension)
    {
        // Replace with actual logic to generate embedding values
        return new float[dimension];
    }
}
