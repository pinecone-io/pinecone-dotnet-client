using System.Text.Json;
using NUnit.Framework;
using Pinecone.Core;

namespace Pinecone.Test.Unit;

[Parallelizable(ParallelScope.All)]
[TestFixture]
public class EmbeddingTest
{
    private const string DenseJson =
        """
        {
          "vector_type": "dense",
          "values": [
            0.123456,
            -0.789012,
            0.345678,
            -0.901234,
            0.567890,
            -0.123456,
            0.789012,
            -0.345678
          ]
        }
        """;

    [Test]
    public void ShouldDeserializeToDense()
    {
        var embedding = JsonUtils.Deserialize<Embedding>(DenseJson);
        Assert.That(embedding, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(embedding.VectorType, Is.EqualTo(VectorType.Dense));
            Assert.That(embedding.Value, Is.InstanceOf<DenseEmbedding>());
            Assert.That(embedding.IsDense, Is.True);
            Assert.That(embedding.IsSparse, Is.False);
            Assert.That(embedding.AsDense(), Is.InstanceOf<DenseEmbedding>());
            Assert.Throws<InvalidCastException>(() => embedding.AsSparse());
        });
    }

    private const string SparseJson =
        """
        {
          "vector_type": "sparse",
          "sparse_values": [
            0.891234,
            0.456789,
            0.234567,
            0.789012,
            0.345678
          ],
          "sparse_indices": [
            12,
            45,
            78,
            156,
            189
          ],
          "sparse_tokens": [
            "machine",
            "learning",
            "artificial",
            "intelligence",
            "neural"
          ]
        }
        """;

    [Test]
    public void ShouldDeserializeToSparse()
    {
        var embedding = JsonUtils.Deserialize<Embedding>(SparseJson);
        Assert.That(embedding, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(embedding.VectorType, Is.EqualTo(VectorType.Sparse));
            Assert.That(embedding.Value, Is.InstanceOf<SparseEmbedding>());
            Assert.That(embedding.IsDense, Is.False);
            Assert.That(embedding.IsSparse, Is.True);
            Assert.That(embedding.AsSparse(), Is.InstanceOf<SparseEmbedding>());
            Assert.Throws<InvalidCastException>(() => embedding.AsDense());
        });
    }

    private const string NullVectorTypeJson =
        """
        {
          "vector_type": null,
          "values": []
        }
        """;

    [Test]
    public void ShouldThrowExceptionForNullVectorType()
    {
        var exception = Assert.Throws<JsonException>(() => JsonUtils.Deserialize<Embedding>(NullVectorTypeJson));
        Assert.That(exception.Message, Is.EqualTo($"Discriminator property '{Embedding.DiscriminatorName}' is null"));
    }

    private const string NoVectorTypeJson =
        """
        {
          "values": []
        }
        """;

    [Test]
    public void ShouldThrowExceptionForMissingVectorType()
    {
        var exception = Assert.Throws<JsonException>(() => JsonUtils.Deserialize<Embedding>(NoVectorTypeJson));
        Assert.That(exception.Message, Is.EqualTo($"Missing discriminator property '{Embedding.DiscriminatorName}'"));
    }

    private const string UnexpectedVectorTypeJson =
        """
        {
          "vector_type": "unexpected",
          "values": []
        }
        """;

    [Test]
    public void ShouldThrowExceptionForUnexpectedVectorType()
    {
        var exception = Assert.Throws<JsonException>(() => JsonUtils.Deserialize<Embedding>(UnexpectedVectorTypeJson));
        Assert.That(exception.Message, Is.EqualTo($"Discriminator property '{Embedding.DiscriminatorName}' is unexpected value 'unexpected'"));
    }

    private const string InvalidVectorTypeJson =
        """
        {
          "vector_type": 123,
          "values": []
        }
        """;

    [Test]
    public void ShouldThrowExceptionForInvalidVectorType()
    {
        var exception = Assert.Throws<JsonException>(() => JsonUtils.Deserialize<Embedding>(InvalidVectorTypeJson));
        Assert.That(exception.Message, Is.EqualTo($"Discriminator property '{Embedding.DiscriminatorName}' is not a string, instead is 123"));
    }

    private const string SparseListJson =
        """
        {
          "model": "multilingual-e5-large",
          "vector_type": "sparse",
          "data": [
            {
              "vector_type": "sparse",
              "sparse_values": [
                0.891234,
                0.456789,
                0.234567,
                0.789012,
                0.345678
              ],
              "sparse_indices": [
                12,
                45,
                78,
                156,
                189
              ],
              "sparse_tokens": [
                "machine",
                "learning",
                "artificial",
                "intelligence",
                "neural"
              ]
            }
          ],
          "usage": {
            "total_tokens": 205
          }
        }
        """;

    [Test]
    public void ShouldDeserializeToSparseList()
    {
        var embeddings = JsonUtils.Deserialize<EmbeddingsList>(SparseListJson);
        Assert.That(embeddings, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(embeddings.Model, Is.EqualTo("multilingual-e5-large"));
            Assert.That(embeddings.VectorType, Is.EqualTo("sparse"));
            Assert.That(embeddings.Data, Is.Not.Null);
            Assert.That(embeddings.Data.Count, Is.EqualTo(1));
            var embedding = embeddings.Data.First();
            Assert.That(embedding, Is.InstanceOf<Embedding>());
            Assert.That(embedding.VectorType, Is.EqualTo(VectorType.Sparse));
            Assert.That(embedding.Value, Is.InstanceOf<SparseEmbedding>());
            Assert.That(embedding.IsDense, Is.False);
            Assert.That(embedding.IsSparse, Is.True);
            Assert.That(embedding.AsSparse(), Is.InstanceOf<SparseEmbedding>());
            Assert.Throws<InvalidCastException>(() => embedding.AsDense());
        });
    }

    private const string DenseListJson =
        """
        {
          "model": "multilingual-e5-large",
          "vector_type": "dense",
          "data": [
            {
              "vector_type": "dense",
              "values": [
                0.123456,
                -0.789012,
                0.345678,
                -0.901234,
                0.567890,
                -0.123456,
                0.789012,
                -0.345678
              ]
            }
          ],
          "usage": {
            "total_tokens": 128
          }
        }
        """;

    [Test]
    public void ShouldDeserializeToDenseList()
    {
        var embeddings = JsonUtils.Deserialize<EmbeddingsList>(DenseListJson);
        Assert.That(embeddings, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(embeddings.Model, Is.EqualTo("multilingual-e5-large"));
            Assert.That(embeddings.VectorType, Is.EqualTo("dense"));
            Assert.That(embeddings.Data, Is.Not.Null);
            Assert.That(embeddings.Data.Count, Is.EqualTo(1));
            var embedding = embeddings.Data.First();
            Assert.That(embedding, Is.InstanceOf<Embedding>());
            Assert.That(embedding.VectorType, Is.EqualTo(VectorType.Dense));
            Assert.That(embedding.Value, Is.InstanceOf<DenseEmbedding>());
            Assert.That(embedding.IsDense, Is.True);
            Assert.That(embedding.IsSparse, Is.False);
            Assert.That(embedding.AsDense(), Is.InstanceOf<DenseEmbedding>());
            Assert.Throws<InvalidCastException>(() => embedding.AsSparse());
        });
    }
}