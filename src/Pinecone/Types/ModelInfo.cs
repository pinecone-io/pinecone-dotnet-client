using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// Represents the model configuration including model type, supported parameters, and other model details.
/// </summary>
public record ModelInfo : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The name of the model.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// A summary of the model.
    /// </summary>
    [JsonPropertyName("short_description")]
    public required string ShortDescription { get; set; }

    /// <summary>
    /// The type of model (e.g. 'embed' or 'rerank').
    /// </summary>
    [JsonPropertyName("type")]
    public required ModelInfoType Type { get; set; }

    /// <summary>
    /// Whether the embedding model produces 'dense' or 'sparse' embeddings.
    /// </summary>
    [JsonPropertyName("vector_type")]
    public ModelInfoVectorType? VectorType { get; set; }

    /// <summary>
    /// The embedding model dimension (applies to dense embedding models only).
    /// </summary>
    [JsonPropertyName("dimension")]
    public int? Dimension { get; set; }

    /// <summary>
    /// The modality of the model (e.g. 'text').
    /// </summary>
    [JsonPropertyName("modality")]
    public string? Modality { get; set; }

    /// <summary>
    /// The maximum tokens per sequence supported by the model.
    /// </summary>
    [JsonPropertyName("sequence_length")]
    public int? SequenceLength { get; set; }

    /// <summary>
    /// The maximum batch size (number of sequences) supported by the model.
    /// </summary>
    [JsonPropertyName("batch_size")]
    public int? BatchSize { get; set; }

    [JsonPropertyName("supported_metrics")]
    public IEnumerable<ModelInfoMetric>? SupportedMetrics { get; set; }

    [JsonPropertyName("supported_parameters")]
    public IEnumerable<ModelInfoSupportedParameter> SupportedParameters { get; set; } =
        new List<ModelInfoSupportedParameter>();

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
