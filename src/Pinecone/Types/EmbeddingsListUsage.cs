using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// Usage statistics for the model inference.
/// </summary>
public record EmbeddingsListUsage
{
    /// <summary>
    /// Total number of tokens consumed across all inputs.
    /// </summary>
    [JsonPropertyName("total_tokens")]
    public int? TotalTokens { get; set; }

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
