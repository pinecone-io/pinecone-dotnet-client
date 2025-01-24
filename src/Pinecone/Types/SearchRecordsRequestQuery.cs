using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

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
    public object? Filter { get; set; }

    [JsonPropertyName("inputs")]
    public object? Inputs { get; set; }

    [JsonPropertyName("vector")]
    public SearchRecordsVector? Vector { get; set; }

    /// <summary>
    /// The unique ID of the vector to be used as a query vector.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
