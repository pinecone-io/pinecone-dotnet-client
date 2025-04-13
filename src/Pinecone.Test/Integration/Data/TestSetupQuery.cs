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

        var results = await IndexClient
            .QueryAsync(
                new QueryRequest
                {
                    Id = "1",
                    Namespace = targetNamespace,
                    TopK = 10,
                }
            )
            .ConfigureAwait(false);

        Assert.That(results, Is.InstanceOf<QueryResponse>());
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));

        Assert.That(results.Usage, Is.Not.Null);
        Assert.That(results.Usage.ReadUnits, Is.Not.Null);
        Assert.That(results.Usage.ReadUnits, Is.GreaterThan(0));

        // By default, does not include values or metadata
        var recordWithMetadata = results.Matches!.FirstOrDefault(match => match.Id == "4");
        Assert.That(recordWithMetadata, Is.Not.Null);
        Assert.That(recordWithMetadata.Metadata, Is.Null);
        Assert.That(recordWithMetadata.Values?.IsEmpty, Is.True);
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByVector(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var results = await IndexClient
            .QueryAsync(
                new QueryRequest
                {
                    Vector = EmbeddingValues(2),
                    Namespace = targetNamespace,
                    TopK = 10,
                }
            )
            .ConfigureAwait(false);

        Assert.That(results, Is.InstanceOf<QueryResponse>());
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByVectorIncludeValues(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var results = await IndexClient
            .QueryAsync(
                new QueryRequest
                {
                    Vector = EmbeddingValues(2),
                    Namespace = targetNamespace,
                    IncludeValues = true,
                    TopK = 10,
                }
            )
            .ConfigureAwait(false);

        Assert.That(results, Is.InstanceOf<QueryResponse>());
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Matches!, Is.Not.Empty);
        Assert.That(results.Matches!.First().Values, Is.Not.Null);
        Assert.That(results.Matches!.First().Values!.Value.Length, Is.EqualTo(ExpectedDimension));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByVectorIncludeMetadata(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var results = await IndexClient
            .QueryAsync(
                new QueryRequest
                {
                    Vector = EmbeddingValues(2),
                    Namespace = targetNamespace,
                    IncludeMetadata = true,
                    TopK = 10,
                }
            )
            .ConfigureAwait(false);

        Assert.That(results, Is.InstanceOf<QueryResponse>());
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));

        var matchesWithMetadata = results.Matches!.Where(match => match.Metadata != null).ToList();
        Assert.That(matchesWithMetadata.Count, Is.EqualTo(3));
        var matchWithGenre = matchesWithMetadata.FirstOrDefault(match => match.Id == "4");
        Assert.That(matchWithGenre, Is.Not.Null);
        Assert.That(matchWithGenre.Metadata!["genre"], Is.EqualTo(new MetadataValue("action")));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByVectorIncludeValuesAndMetadata(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var results = await IndexClient
            .QueryAsync(
                new QueryRequest
                {
                    Vector = EmbeddingValues(2),
                    Namespace = targetNamespace,
                    IncludeValues = true,
                    IncludeMetadata = true,
                    TopK = 10,
                }
            )
            .ConfigureAwait(false);

        Assert.That(results, Is.InstanceOf<QueryResponse>());
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));

        var matchesWithMetadata = results.Matches!.Where(match => match.Metadata != null).ToList();
        Assert.That(matchesWithMetadata.Count, Is.EqualTo(3));
        var matchWithGenre = matchesWithMetadata.FirstOrDefault(match => match.Id == "4");
        Assert.That(matchWithGenre, Is.Not.Null);
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
