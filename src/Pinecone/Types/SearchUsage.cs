using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
