using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The result of a reranking request.
/// </summary>
[Serializable]
public record RerankResult : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The model used to rerank documents.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// The reranked documents.
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<RankedDocument> Data { get; set; } = new List<RankedDocument>();

    /// <summary>
    /// Usage statistics for the model inference.
    /// </summary>
    [JsonPropertyName("usage")]
    public required RerankResultUsage Usage { get; set; }

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
