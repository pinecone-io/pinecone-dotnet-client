using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

[JsonConverter(typeof(EmbeddingConverter))]
public record Embedding
{
    internal const string DiscriminatorName = "vector_type";

    public Embedding(DenseEmbedding embedding)
    {
        VectorType = VectorType.Dense;
        Value = embedding;
    }

    public Embedding(SparseEmbedding embedding)
    {
        VectorType = VectorType.Sparse;
        Value = embedding;
    }

    public VectorType VectorType { get; }
    public object Value { get; }
    public bool IsDense => VectorType == VectorType.Dense;
    public bool IsSparse => VectorType == VectorType.Sparse;
    public DenseEmbedding AsDense() => (DenseEmbedding)Value;
    public SparseEmbedding AsSparse() => (SparseEmbedding)Value;
    public static implicit operator Embedding(DenseEmbedding embedding) => new(embedding);
    public static implicit operator Embedding(SparseEmbedding embedding) => new(embedding);

    public T Match<T>(Func<DenseEmbedding, T> onDense, Func<SparseEmbedding, T> onSparse)
    {
        switch (VectorType)
        {
            case VectorType.Dense:
                return onDense(AsDense());
            case VectorType.Sparse:
                return onSparse(AsSparse());
            default:
                throw new Exception("Unexpected VectorType");
        }
    }

    public void Visit(Action<DenseEmbedding> onDense, Action<SparseEmbedding> onSparse)
    {
        switch (VectorType)
        {
            case VectorType.Dense:
                onDense(AsDense());
                break;
            case VectorType.Sparse:
                onSparse(AsSparse());
                break;
            default:
                throw new Exception("Unexpected VectorType");
        }
    }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}

internal class EmbeddingConverter : JsonConverter<Embedding>
{
    public override bool CanConvert(Type typeToConvert) => typeof(Embedding).IsAssignableFrom(typeToConvert);

    public override Embedding Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonObject = JsonElement.ParseValue(ref reader);
        if (!jsonObject.TryGetProperty(Embedding.DiscriminatorName, out var discriminatorElement))
        {
            throw new JsonException($"Missing discriminator property '{Embedding.DiscriminatorName}'");
        }

        if (discriminatorElement.ValueKind != JsonValueKind.String)
        {
            if (discriminatorElement.ValueKind == JsonValueKind.Null)
            {
                throw new JsonException($"Discriminator property '{Embedding.DiscriminatorName}' is null");
            }

            throw new JsonException(
                $"Discriminator property '{Embedding.DiscriminatorName}' is not a string, instead is {discriminatorElement.ToString()}");
        }

        var discriminator = discriminatorElement.GetString() ??
                            throw new JsonException($"Discriminator property '{Embedding.DiscriminatorName}' is null");
        switch (discriminator)
        {
            case "dense":
                var denseEmbedding = jsonObject.Deserialize<DenseEmbedding>()
                                     ?? throw new JsonException("Failed to deserialize DenseEmbedding");
                return new Embedding(denseEmbedding);
            case "sparse":
                var sparseEmbedding = jsonObject.Deserialize<SparseEmbedding>()
                                      ?? throw new JsonException("Failed to deserialize DenseEmbedding");
                return new Embedding(sparseEmbedding);
            default:
                throw new JsonException(
                    $"Discriminator property '{Embedding.DiscriminatorName}' is unexpected value '{discriminator}'");
        }
    }

    public override void Write(
        Utf8JsonWriter writer, Embedding embedding, JsonSerializerOptions options)
    {
        var jsonNode = JsonSerializer.SerializeToNode(embedding.Value, options);
        if (jsonNode == null)
        {
            throw new JsonException("Failed to serialize Embedding");
        }
        jsonNode.WriteTo(writer, options);
    }
}