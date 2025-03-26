using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The embedding model and document fields mapped to embedding inputs.
/// </summary>
public record ModelIndexEmbed
{
    /// <summary>
    /// The name of the embedding model used to create the index.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// The distance metric to be used for similarity search. You can use 'euclidean', 'cosine', or 'dotproduct'. If not specified, the metric will be defaulted according to the model. Cannot be updated once set.
    /// </summary>
    [JsonPropertyName("metric")]
    public ModelIndexEmbedMetric? Metric { get; set; }

    /// <summary>
    /// The dimensions of the vectors to be inserted in the index.
    /// </summary>
    [JsonPropertyName("dimension")]
    public int? Dimension { get; set; }

    /// <summary>
    /// The index vector type. You can use 'dense' or 'sparse'. If 'dense', the vector dimension must be specified.  If 'sparse', the vector dimension should not be specified.
    /// </summary>
    [JsonPropertyName("vector_type")]
    public VectorType? VectorType { get; set; }

    /// <summary>
    /// Identifies the name of the text field from your document model that is embedded.
    /// </summary>
    [JsonPropertyName("field_map")]
    public Dictionary<string, object?>? FieldMap { get; set; }

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
