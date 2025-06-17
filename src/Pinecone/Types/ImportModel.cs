using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The model for an import operation.
/// </summary>
[Serializable]
public record ImportModel : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Unique identifier for the import operation.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The URI from where the data is imported.
    /// </summary>
    [JsonPropertyName("uri")]
    public string? Uri { get; set; }

    /// <summary>
    /// The status of the operation.
    /// </summary>
    [JsonPropertyName("status")]
    public ImportModelStatus? Status { get; set; }

    /// <summary>
    /// The start time of the import operation.
    /// </summary>
    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// The end time of the import operation.
    /// </summary>
    [JsonPropertyName("finishedAt")]
    public DateTime? FinishedAt { get; set; }

    /// <summary>
    /// The progress made by the operation, as a percentage.
    /// </summary>
    [JsonPropertyName("percentComplete")]
    public float? PercentComplete { get; set; }

    /// <summary>
    /// The number of records successfully imported.
    /// </summary>
    [JsonPropertyName("recordsImported")]
    public long? RecordsImported { get; set; }

    /// <summary>
    /// The error message if the import process failed.
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

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
