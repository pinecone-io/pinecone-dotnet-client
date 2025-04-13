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
        var e = Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            var index = pinecone.Index(null, IndexHost);
            await index
                .UpsertAsync(
                    new UpsertRequest
                    {
                        Vectors = new List<Vector>
                        {
                            new() { Id = "1", Values = Helpers.EmbeddingValues(2) },
                            new() { Id = "2", Values = Helpers.EmbeddingValues(3) },
                        },
                    }
                )
                .ConfigureAwait(false);
        });
        Assert.That(e.StatusCode, Is.EqualTo(7));
    }

    [Test]
    public void TestUpsertFailsWhenDimensionMismatch()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient
                .UpsertAsync(
                    new UpsertRequest
                    {
                        Vectors = new List<Vector>
                        {
                            new() { Id = "1", Values = Helpers.EmbeddingValues(2) },
                            new() { Id = "2", Values = Helpers.EmbeddingValues(3) },
                        },
                    }
                )
                .ConfigureAwait(false);
        });
    }

    [Test]
    public void TestUpsertFailsWhenSparseValuesIndicesValuesMismatch()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient
                .UpsertAsync(
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
                                    Values = Helpers.EmbeddingValues(2),
                                },
                            },
                        },
                    }
                )
                .ConfigureAwait(false);
        });

        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient
                .UpsertAsync(
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
                                    Values = Helpers.EmbeddingValues(1),
                                },
                            },
                        },
                    }
                )
                .ConfigureAwait(false);
        });
    }

    [Test]
    public void TestUpsertFailsWhenSparseValuesInTuples()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient
                .UpsertAsync(
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
                                    Values = Helpers.EmbeddingValues(1),
                                },
                            },
                            new()
                            {
                                Id = "2",
                                SparseValues = new SparseValues
                                {
                                    Indices = new List<uint> { 0, 1, 2 },
                                    Values = Helpers.EmbeddingValues(3),
                                },
                            },
                        },
                    }
                )
                .ConfigureAwait(false);
        });
    }

    [Test]
    public void TestUpsertFailsWhenValuesMissing()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient
                .UpsertAsync(
                    new UpsertRequest
                    {
                        Vectors = new List<Vector>
                        {
                            new() { Id = "1" },
                            new() { Id = "2" },
                        },
                    }
                )
                .ConfigureAwait(false);
        });
    }

    [Test]
    public void TestUpsertFailsWhenVectorsEmpty()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient
                .UpsertAsync(new UpsertRequest { Vectors = new List<Vector>() })
                .ConfigureAwait(false);
        });
    }

    [Test]
    public void TestUpsertFailsWhenVectorsMissing()
    {
        Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient
                .UpsertAsync(
                    new UpsertRequest
                    {
                        // Missing the Vectors field entirely
                    }
                )
                .ConfigureAwait(false);
        });
    }
}
