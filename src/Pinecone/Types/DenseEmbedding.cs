using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// A dense embedding of a single input
/// </summary>
public record DenseEmbedding
{
    /// <summary>
    /// The dense embedding values.
    /// </summary>
    [JsonPropertyName("values")]
    public ReadOnlyMemory<float> Values { get; set; }

    [JsonPropertyName("vector_type")]
    public required VectorType VectorType { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
