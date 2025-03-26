using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The response for the `list_imports` operation.
/// </summary>
public record ListImportsResponse
{
    [JsonPropertyName("data")]
    public IEnumerable<ImportModel>? Data { get; set; }

    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; set; }

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
