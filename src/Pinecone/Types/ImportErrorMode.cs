using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// Indicates how to respond to errors during the import process.
/// </summary>
public record ImportErrorMode
{
    /// <summary>
    /// Indicates how to respond to errors during the import process.
    /// </summary>
    [JsonPropertyName("onError")]
    public ImportErrorModeOnError? OnError { get; set; }

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
