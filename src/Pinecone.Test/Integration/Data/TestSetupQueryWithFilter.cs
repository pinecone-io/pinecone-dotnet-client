using NUnit.Framework;

namespace Pinecone.Test.Integration;

public class TestSetupQueryWithFilter : BaseDataPlaneTest
{
    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByIdWithFilter(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? _namespace : "";

        var results = await _indexClient.QueryAsync(
            new QueryRequest
            {
                Id = "1",
                Namespace = targetNamespace,
                Filter = new Metadata { { "genre", "action" } },
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Matches!.Count, Is.EqualTo(1));
        Assert.That(results.Matches!.First().Id, Is.EqualTo("4"));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByIdWithFilterGt(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? _namespace : "";

        var results = await _indexClient.QueryAsync(
            new QueryRequest
            {
                Id = "1",
                Namespace = targetNamespace,
                Filter = new Metadata
                {
                    {
                        "runtime",
                        new MetadataValue(new Metadata { { "$gt", 100 } })
                    }
                },
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Matches!.Count, Is.EqualTo(2));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "4"));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "6"));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByIdWithFilterGte(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? _namespace : "";

        var results = await _indexClient.QueryAsync(
            new QueryRequest
            {
                Id = "1",
                Namespace = targetNamespace,
                Filter = new Metadata
                {
                    {
                        "runtime",
                        new MetadataValue(new Metadata { { "$gte", 90 } })
                    }
                },
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Matches!.Count, Is.EqualTo(3));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "4"));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "5"));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "6"));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByIdWithFilterLt(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? _namespace : "";

        var results = await _indexClient.QueryAsync(
            new QueryRequest
            {
                Id = "1",
                Namespace = targetNamespace,
                Filter = new Metadata
                {
                    {
                        "runtime",
                        new MetadataValue(new Metadata { { "$lt", 100 } })
                    }
                },
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Matches!.Count, Is.EqualTo(1));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "5"));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByIdWithFilterLte(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? _namespace : "";

        var results = await _indexClient.QueryAsync(
            new QueryRequest
            {
                Id = "1",
                Namespace = targetNamespace,
                Filter = new Metadata
                {
                    {
                        "runtime",
                        new MetadataValue(
                            new Metadata { { "$lte", 120 } }
                        )
                    }
                },
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Matches!.Count, Is.EqualTo(2));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "4"));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "5"));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByIdWithFilterIn(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? _namespace : "";

        var results = await _indexClient.QueryAsync(
            new QueryRequest
            {
                Id = "1",
                Namespace = targetNamespace,
                Filter = new Metadata
                {
                    {
                        "genre",
                        new MetadataValue(
                            new Metadata
                            {
                                {
                                    "$in",
                                    new List<string> { "romance" }
                                }
                            }
                        )
                    }
                },
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Matches!.Count, Is.EqualTo(1));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "6"));
    }

    [TestCase(true)]
    [TestCase(false)]
    [Ignore("Seems like a bug in the server")]
    public async Task TestQueryByIdWithFilterNin(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? _namespace : "";

        var results = await _indexClient.QueryAsync(
            new QueryRequest
            {
                Id = "1",
                Namespace = targetNamespace,
                Filter = new Metadata
                {
                    {
                        "genre",
                        new MetadataValue(
                            new Metadata
                            {
                                {
                                    "$nin",
                                    new List<string> { "romance" }
                                }
                            }
                        )
                    }
                },
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Matches!.Count, Is.EqualTo(2));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "4"));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "5"));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestQueryByIdWithFilterEq(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? _namespace : "";

        var results = await _indexClient.QueryAsync(
            new QueryRequest
            {
                Id = "1",
                Namespace = targetNamespace,
                Filter = new Metadata
                {
                    {
                        "genre",
                        new MetadataValue(
                            new Metadata { { "$eq", "action" } }
                        )
                    }
                },
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Matches!.Count, Is.EqualTo(1));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "4"));
    }

    [TestCase(true)]
    [TestCase(false)]
    [Ignore("Seems like a bug in the server")]
    public async Task TestQueryByIdWithFilterNe(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? _namespace : "";

        var results = await _indexClient.QueryAsync(
            new QueryRequest
            {
                Id = "1",
                Namespace = targetNamespace,
                Filter = new Metadata
                {
                    {
                        "genre",
                        new MetadataValue(
                            new Metadata { { "$ne", "action" } }
                        )
                    }
                },
                TopK = 10
            }
        );

        Assert.IsInstanceOf<QueryResponse>(results);
        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Matches!.Count, Is.EqualTo(2));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "5"));
        Assert.IsNotNull(results.Matches!.FirstOrDefault(m => m.Id == "6"));
    }
}