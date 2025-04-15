using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

public record SearchRecordsVector : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
