using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// Specify the integrated inference embedding configuration for the index.
///
/// Once set the model cannot be changed, but you can later update the embedding configuration for an integrated inference index including field map, read parameters, or write parameters.
///
/// Refer to the [model guide](https://docs.pinecone.io/guides/index-data/create-an-index#embedding-models) for available models and model details.
/// </summary>
public record CreateIndexForModelRequestEmbed : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The name of the embedding model to use for the index.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// The distance metric to be used for similarity search. You can use 'euclidean', 'cosine', or 'dotproduct'. If not specified, the metric will be defaulted according to the model. Cannot be updated once set.
    /// </summary>
    [JsonPropertyName("metric")]
    public MetricType? Metric { get; set; }

    /// <summary>
    /// Identifies the name of the text field from your document model that will be embedded.
    /// </summary>
    [JsonPropertyName("field_map")]
    public Dictionary<string, object?> FieldMap { get; set; } = new Dictionary<string, object?>();

    /// <summary>
    /// The dimension of embedding vectors produced for the index.
    /// </summary>
    [JsonPropertyName("dimension")]
    public int? Dimension { get; set; }

    /// <summary>
    /// The read parameters for the embedding model.
    /// </summary>
    [JsonPropertyName("read_parameters")]
    public Dictionary<string, object?>? ReadParameters { get; set; }

    /// <summary>
    /// The write parameters for the embedding model.
    /// </summary>
    [JsonPropertyName("write_parameters")]
    public Dictionary<string, object?>? WriteParameters { get; set; }

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
