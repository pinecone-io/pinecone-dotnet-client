using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record DenseEmbedding
{
    /// <summary>
    /// The dense embedding values.
    /// </summary>
    [JsonPropertyName("values")]
    public ReadOnlyMemory<float> Values { get; set; }

    [JsonPropertyName("vector_type")]
    public required VectorType VectorType { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
