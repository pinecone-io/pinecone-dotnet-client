using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// .
/// </summary>
public record SearchRecordsRequestQuery : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The number of similar records to return.
    /// </summary>
    [JsonPropertyName("top_k")]
    public required int TopK { get; set; }

    /// <summary>
    /// The filter to apply. You can use vector metadata to limit your search. See [Understanding metadata](https://docs.pinecone.io/guides/index-data/indexing-overview#metadata).
    /// </summary>
    [JsonPropertyName("filter")]
    public Dictionary<string, object?>? Filter { get; set; }

    [JsonPropertyName("inputs")]
    public Dictionary<string, object?>? Inputs { get; set; }

    [JsonPropertyName("vector")]
    public SearchRecordsVector? Vector { get; set; }

    /// <summary>
    /// The unique ID of the vector to be used as a query vector.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

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
