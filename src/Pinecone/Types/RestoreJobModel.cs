using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The RestoreJobModel describes the status of a restore job.
/// </summary>
[Serializable]
public record RestoreJobModel : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier for the restore job
    /// </summary>
    [JsonPropertyName("restore_job_id")]
    public required string RestoreJobId { get; set; }

    /// <summary>
    /// Backup used for the restore
    /// </summary>
    [JsonPropertyName("backup_id")]
    public required string BackupId { get; set; }

    /// <summary>
    /// Name of the index into which data is being restored
    /// </summary>
    [JsonPropertyName("target_index_name")]
    public required string TargetIndexName { get; set; }

    /// <summary>
    /// ID of the index
    /// </summary>
    [JsonPropertyName("target_index_id")]
    public required string TargetIndexId { get; set; }

    /// <summary>
    /// Status of the restore job
    /// </summary>
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    /// <summary>
    /// Timestamp when the restore job started
    /// </summary>
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// Timestamp when the restore job finished
    /// </summary>
    [JsonPropertyName("completed_at")]
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// The progress made by the restore job out of 100
    /// </summary>
    [JsonPropertyName("percent_complete")]
    public float? PercentComplete { get; set; }

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
