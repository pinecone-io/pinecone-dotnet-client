using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// A record whose vector values are similar to the provided search query.
/// </summary>
public record Hit : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The record id of the search hit.
    /// </summary>
    [JsonPropertyName("_id")]
    public required string Id { get; set; }

    /// <summary>
    /// The similarity score of the returned record.
    /// </summary>
    [JsonPropertyName("_score")]
    public required float Score { get; set; }

    /// <summary>
    /// The selected record fields associated with the search hit.
    /// </summary>
    [JsonPropertyName("fields")]
    public Dictionary<string, object?> Fields { get; set; } = new Dictionary<string, object?>();

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
