using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

public record SearchRecordsVector
{
    [JsonPropertyName("values")]
    public ReadOnlyMemory<float>? Values { get; set; }

    /// <summary>
    /// The sparse embedding values.
    /// </summary>
    [JsonPropertyName("sparse_values")]
    public ReadOnlyMemory<float>? SparseValues { get; set; }

    /// <summary>
    /// The sparse embedding indices.
    /// </summary>
    [JsonPropertyName("sparse_indices")]
    public IEnumerable<int>? SparseIndices { get; set; }

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
