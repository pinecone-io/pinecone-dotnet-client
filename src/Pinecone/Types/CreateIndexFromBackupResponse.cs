using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The response for creating an index from a backup.
/// </summary>
public record CreateIndexFromBackupResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The ID of the restore job that was created.
    /// </summary>
    [JsonPropertyName("restore_job_id")]
    public required string RestoreJobId { get; set; }

    /// <summary>
    /// The ID of the index that was created from the backup.
    /// </summary>
    [JsonPropertyName("index_id")]
    public required string IndexId { get; set; }

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
