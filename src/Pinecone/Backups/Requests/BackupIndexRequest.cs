using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

public record BackupIndexRequest
{
    /// <summary>
    /// The name of the backup.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// A description of the backup.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
