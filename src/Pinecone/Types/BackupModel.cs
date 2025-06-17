using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The BackupModel describes the configuration and status of a Pinecone backup.
/// </summary>
[Serializable]
public record BackupModel : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier for the backup.
    /// </summary>
    [JsonPropertyName("backup_id")]
    public required string BackupId { get; set; }

    /// <summary>
    /// Name of the index from which the backup was taken.
    /// </summary>
    [JsonPropertyName("source_index_name")]
    public required string SourceIndexName { get; set; }

    /// <summary>
    /// ID of the index.
    /// </summary>
    [JsonPropertyName("source_index_id")]
    public required string SourceIndexId { get; set; }

    /// <summary>
    /// Optional user-defined name for the backup.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Optional description providing context for the backup.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Current status of the backup (e.g., Initializing, Ready, Failed).
    /// </summary>
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    /// <summary>
    /// Cloud provider where the backup is stored.
    /// </summary>
    [JsonPropertyName("cloud")]
    public required string Cloud { get; set; }

    /// <summary>
    /// Cloud region where the backup is stored.
    /// </summary>
    [JsonPropertyName("region")]
    public required string Region { get; set; }

    /// <summary>
    /// The dimensions of the vectors to be inserted in the index.
    /// </summary>
    [JsonPropertyName("dimension")]
    public int? Dimension { get; set; }

    /// <summary>
    /// The distance metric to be used for similarity search. You can use 'euclidean', 'cosine', or 'dotproduct'. If the 'vector_type' is 'sparse', the metric must be 'dotproduct'. If the `vector_type` is `dense`, the metric defaults to 'cosine'.
    /// </summary>
    [JsonPropertyName("metric")]
    public MetricType? Metric { get; set; }

    /// <summary>
    /// Total number of records in the backup.
    /// </summary>
    [JsonPropertyName("record_count")]
    public int? RecordCount { get; set; }

    /// <summary>
    /// Number of namespaces in the backup.
    /// </summary>
    [JsonPropertyName("namespace_count")]
    public int? NamespaceCount { get; set; }

    /// <summary>
    /// Size of the backup in bytes.
    /// </summary>
    [JsonPropertyName("size_bytes")]
    public int? SizeBytes { get; set; }

    [JsonPropertyName("tags")]
    public Dictionary<string, string>? Tags { get; set; }

    /// <summary>
    /// Timestamp when the backup was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

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
