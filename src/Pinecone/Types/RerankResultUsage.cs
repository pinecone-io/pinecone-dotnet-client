using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// Usage statistics for the model inference.
/// </summary>
public record RerankResultUsage
{
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
