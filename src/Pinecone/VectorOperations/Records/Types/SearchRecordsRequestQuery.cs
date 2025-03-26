using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.VectorOperations;

/// <summary>
/// The query inputs to search with.
/// </summary>
public record SearchRecordsRequestQuery
{
    /// <summary>
    /// The number of results to return for each search.
    /// </summary>
    [JsonPropertyName("top_k")]
    public required int TopK { get; set; }

    /// <summary>
    /// The filter to apply.
    /// </summary>
    [JsonPropertyName("filter")]
    public Dictionary<string, object?>? Filter { get; set; }

    [JsonPropertyName("inputs")]
    public Dictionary<string, object?>? Inputs { get; set; }

    [JsonPropertyName("vector")]
    public SearchRecordsVector? Vector { get; set; }

    /// <summary>
    /// The unique ID of the vector to be used as a query vector.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

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
