using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The CollectionModel describes the configuration and status of a Pinecone collection.
/// </summary>
[Serializable]
public record CollectionModel : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The name of the collection.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// The size of the collection in bytes.
    /// </summary>
    [JsonPropertyName("size")]
    public long? Size { get; set; }

    /// <summary>
    /// The status of the collection.
    /// </summary>
    [JsonPropertyName("status")]
    public required CollectionModelStatus Status { get; set; }

    /// <summary>
    /// The dimension of the vectors stored in each record held in the collection.
    /// </summary>
    [JsonPropertyName("dimension")]
    public int? Dimension { get; set; }

    /// <summary>
    /// The number of records stored in the collection.
    /// </summary>
    [JsonPropertyName("vector_count")]
    public int? VectorCount { get; set; }

    /// <summary>
    /// The environment where the collection is hosted.
    /// </summary>
    [JsonPropertyName("environment")]
    public required string Environment { get; set; }

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
