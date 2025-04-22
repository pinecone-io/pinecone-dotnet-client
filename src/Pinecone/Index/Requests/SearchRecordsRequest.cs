using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

public record SearchRecordsRequest
{
    /// <summary>
    /// .
    /// </summary>
    [JsonPropertyName("query")]
    public required SearchRecordsRequestQuery Query { get; set; }

    /// <summary>
    /// The fields to return in the search results. If not specified, the response will include all fields.
    /// </summary>
    [JsonPropertyName("fields")]
    public IEnumerable<string>? Fields { get; set; }

    /// <summary>
    /// Parameters for reranking the initial search results.
    /// </summary>
    [JsonPropertyName("rerank")]
    public SearchRecordsRequestRerank? Rerank { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
