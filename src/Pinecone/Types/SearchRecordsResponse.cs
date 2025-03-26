using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The records search response.
/// </summary>
public record SearchRecordsResponse
{
    [JsonPropertyName("result")]
    public required SearchRecordsResponseResult Result { get; set; }

    [JsonPropertyName("usage")]
    public required SearchUsage Usage { get; set; }

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
