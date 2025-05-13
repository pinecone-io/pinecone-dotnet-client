using System.Text.Json;
using System.Text.Json.Serialization;
using OneOf;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The IndexModel describes the configuration and status of a Pinecone index.
/// </summary>
public record Index : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The name of the index. Resource name must be 1-45 characters long, start and end with an alphanumeric character, and consist only of lower case alphanumeric characters or '-'.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// The dimensions of the vectors to be inserted in the index.
    /// </summary>
    [JsonPropertyName("dimension")]
    public int? Dimension { get; set; }

    /// <summary>
    /// The distance metric to be used for similarity search. You can use 'euclidean', 'cosine', or 'dotproduct'. If the 'vector_type' is 'sparse', the metric must be 'dotproduct'. If the `vector_type` is `dense`, the metric defaults to 'cosine'.
    /// </summary>
    [JsonPropertyName("metric")]
    public required IndexModelMetric Metric { get; set; }

    /// <summary>
    /// The URL address where the index is hosted.
    /// </summary>
    [JsonPropertyName("host")]
    public required string Host { get; set; }

    [JsonPropertyName("deletion_protection")]
    public DeletionProtection? DeletionProtection { get; set; }

    [JsonPropertyName("tags")]
    public Dictionary<string, string>? Tags { get; set; }

    [JsonPropertyName("embed")]
    public ModelIndexEmbed? Embed { get; set; }

    [JsonPropertyName("spec")]
    public required OneOf<ServerlessIndexSpec, PodIndexSpec, ByocIndexSpec> Spec { get; set; }

    [JsonPropertyName("status")]
    public required IndexModelStatus Status { get; set; }

    /// <summary>
    /// The index vector type. You can use 'dense' or 'sparse'. If 'dense', the vector dimension must be specified.  If 'sparse', the vector dimension should not be specified.
    /// </summary>
    [JsonPropertyName("vector_type")]
    public required VectorType VectorType { get; set; }

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
