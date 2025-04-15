using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// A ranked document with a relevance score and an index position.
/// </summary>
public record RankedDocument : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The index position of the document from the original request.
    /// </summary>
    [JsonPropertyName("index")]
    public required int Index { get; set; }

    /// <summary>
    /// The relevance of the document to the query, normalized between 0 and 1, with scores closer to 1 indicating higher relevance.
    /// </summary>
    [JsonPropertyName("score")]
    public required double Score { get; set; }

    [JsonPropertyName("document")]
    public Dictionary<string, object?>? Document { get; set; }

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
