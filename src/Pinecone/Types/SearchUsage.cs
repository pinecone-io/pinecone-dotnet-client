using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

public record SearchUsage : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The number of read units consumed by this operation.
    /// </summary>
    [JsonPropertyName("read_units")]
    public required int ReadUnits { get; set; }

    /// <summary>
    /// The number of embedding tokens consumed by this operation.
    /// </summary>
    [JsonPropertyName("embed_total_tokens")]
    public int? EmbedTotalTokens { get; set; }

    /// <summary>
    /// The number of rerank units consumed by this operation.
    /// </summary>
    [JsonPropertyName("rerank_units")]
    public int? RerankUnits { get; set; }

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
