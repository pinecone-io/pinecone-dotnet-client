using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

public record SearchUsage
{
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
