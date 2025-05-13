using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The list of backups that exist in the project.
/// </summary>
public record BackupList : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("data")]
    public IEnumerable<BackupModel>? Data { get; set; }

    [JsonPropertyName("pagination")]
    public PaginationResponse? Pagination { get; set; }

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
