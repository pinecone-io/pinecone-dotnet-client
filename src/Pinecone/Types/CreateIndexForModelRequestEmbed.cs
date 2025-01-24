using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record CreateIndexForModelRequestEmbed
{
    /// <summary>
    /// The name of the embedding model to use for the index.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// The distance metric to be used for similarity search. You can use 'euclidean', 'cosine', or 'dotproduct'. If not specified, the metric will be defaulted according to the model. Cannot be updated once set.
    /// </summary>
    [JsonPropertyName("metric")]
    public CreateIndexForModelRequestEmbedMetric? Metric { get; set; }

    /// <summary>
    /// Identifies the name of the text field from your document model that will be embedded.
    /// </summary>
    [JsonPropertyName("field_map")]
    public object FieldMap { get; set; } = new Dictionary<string, object?>();

    /// <summary>
    /// The read parameters for the embedding model.
    /// </summary>
    [JsonPropertyName("read_parameters")]
    public object? ReadParameters { get; set; }

    /// <summary>
    /// The write parameters for the embedding model.
    /// </summary>
    [JsonPropertyName("write_parameters")]
    public object? WriteParameters { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
