using Grpc.Core;
using NUnit.Framework;
using Pinecone.Core;

namespace Pinecone.Test.Integration.Data;

public class TestSetupUpsertErrors : BaseTest
{
    [Test]
    public void TestUpsertFailsWhenApiKeyInvalid()
    {
        var pinecone = new PineconeClient(Helpers.FakeApiKey());

        var e = Assert.Throws<PineconeApiException>(() =>
        {
            pinecone.Index(IndexName);
        });
        Assert.That(e.StatusCode, Is.EqualTo(401));
    }

    [Test]
    public void TestUpsertFailsWhenDimensionMismatchObjects()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(
                new UpsertRequest
                {
                    Vectors = new List<Vector>
                    {
                        new() { Id = "1", Values = Helpers.EmbeddingValues(2) },
                        new() { Id = "2", Values = Helpers.EmbeddingValues(3) }
                    }
                }
            );
        });
    }

    [Test]
    public void TestUpsertFailsWhenDimensionMismatchTuples()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(
                new UpsertRequest
                {
                    Vectors = new List<Vector>
                    {
                        new() { Id = "1", Values = Helpers.EmbeddingValues(2) },
                        new() { Id = "2", Values = Helpers.EmbeddingValues(3) }
                    }
                }
            );
        });
    }

    [Test]
    public void TestUpsertFailsWhenDimensionMismatchDicts()
    {
        var e = Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(
                new UpsertRequest
                {
                    Vectors = new List<Vector>
                    {
                        new() { Id = "1", Values = Helpers.EmbeddingValues(2) },
                        new() { Id = "2", Values = Helpers.EmbeddingValues(3) }
                    }
                }
            );
        });
    }

    [Test]
    public void TestUpsertFailsWhenSparseValuesIndicesValuesMismatchObjects()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(
                new UpsertRequest
                {
                    Vectors = new List<Vector>
                    {
                        new()
                        {
                            Id = "1",
                            Values = Helpers.EmbeddingValues(2),
                            SparseValues = new SparseValues
                            {
                                Indices = new List<uint> { 0 },
                                Values = Helpers.EmbeddingValues(2)
                            }
                        }
                    }
                }
            );
        });

        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(
                new UpsertRequest
                {
                    Vectors = new List<Vector>
                    {
                        new()
                        {
                            Id = "1",
                            Values = Helpers.EmbeddingValues(2),
                            SparseValues = new SparseValues
                            {
                                Indices = new List<uint> { 0, 1 },
                                Values = Helpers.EmbeddingValues(1)
                            }
                        }
                    }
                }
            );
        });
    }

    [Test]
    public void TestUpsertFailsWhenSparseValuesInTuples()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(
                new UpsertRequest
                {
                    Vectors = new List<Vector>
                    {
                        new()
                        {
                            Id = "1",
                            SparseValues = new SparseValues
                            {
                                Indices = new List<uint> { 0 },
                                Values = Helpers.EmbeddingValues(1)
                            }
                        },
                        new()
                        {
                            Id = "2",
                            SparseValues = new SparseValues
                            {
                                Indices = new List<uint> { 0, 1, 2 },
                                Values = Helpers.EmbeddingValues(3)
                            }
                        }
                    }
                }
            );
        });
    }

    [Test]
    public void TestUpsertFailsWhenSparseValuesIndicesValuesMismatchDicts()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(
                new UpsertRequest
                {
                    Vectors = new List<Vector>
                    {
                        new()
                        {
                            Id = "1",
                            Values = Helpers.EmbeddingValues(2),
                            SparseValues = new SparseValues
                            {
                                Indices = new List<uint> { 0 },
                                Values = Helpers.EmbeddingValues(2)
                            }
                        }
                    }
                }
            );
        });

        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(
                new UpsertRequest
                {
                    Vectors = new List<Vector>
                    {
                        new()
                        {
                            Id = "1",
                            Values = Helpers.EmbeddingValues(2),
                            SparseValues = new SparseValues
                            {
                                Indices = new List<uint> { 0, 1 },
                                Values = Helpers.EmbeddingValues(1)
                            }
                        }
                    }
                }
            );
        });
    }

    [Test]
    public void TestUpsertFailsWhenValuesMissingObjects()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(
                new UpsertRequest
                {
                    Vectors = new List<Vector>
                    {
                        new() { Id = "1" },
                        new() { Id = "2" }
                    }
                }
            );
        });
    }

    [Test]
    public void TestUpsertFailsWhenValuesMissingTuples()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(
                new UpsertRequest
                {
                    Vectors = new List<Vector>
                    {
                        new() { Id = "1" },
                        new() { Id = "2" }
                    }
                }
            );
        });
    }

    [Test]
    public void TestUpsertFailsWhenValuesMissingDicts()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(
                new UpsertRequest
                {
                    Vectors = new List<Vector>
                    {
                        new() { Id = "1" },
                        new() { Id = "2" }
                    }
                }
            );
        });
    }

    [Test]
    public void TestUpsertFailsWhenVectorsEmpty()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(new UpsertRequest { Vectors = new List<Vector>() });
        });
    }

    [Test]
    public void TestUpsertFailsWhenVectorsMissing()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.UpsertAsync(
                new UpsertRequest
                {
                    // Missing the Vectors field entirely
                }
            );
        });
    }
}
