using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record SparseEmbedding
{
    /// <summary>
    /// The sparse embedding values.
    /// </summary>
    [JsonPropertyName("sparse_values")]
    public ReadOnlyMemory<float> SparseValues { get; set; }

    /// <summary>
    /// The sparse embedding indices.
    /// </summary>
    [JsonPropertyName("sparse_indices")]
    public IEnumerable<int> SparseIndices { get; set; } = new List<int>();

    /// <summary>
    /// The normalized tokens used to create the sparse embedding.
    /// </summary>
    [JsonPropertyName("sparse_tokens")]
    public IEnumerable<string>? SparseTokens { get; set; }

    [JsonPropertyName("vector_type")]
    public required VectorType VectorType { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
