using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The response for the `start_import` operation.
/// </summary>
public record StartImportResponse
{
    /// <summary>
    /// Unique identifier for the import operation.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

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
