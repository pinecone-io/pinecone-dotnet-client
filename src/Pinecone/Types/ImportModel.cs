using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The model for an import operation.
/// </summary>
public record ImportModel
{
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
