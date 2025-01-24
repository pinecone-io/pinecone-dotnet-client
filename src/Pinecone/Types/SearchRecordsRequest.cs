using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record SearchRecordsRequest
{
    /// <summary>
    /// The query inputs to search with.
    /// </summary>
    [JsonPropertyName("query")]
    public required SearchRecordsRequestQuery Query { get; set; }

    /// <summary>
    /// The fields to return in the search results.
    /// </summary>
    [JsonPropertyName("fields")]
    public IEnumerable<string>? Fields { get; set; }

    /// <summary>
    /// Parameters for reranking the initial search results.
    /// </summary>
    [JsonPropertyName("rerank")]
    public SearchRecordsRequestRerank? Rerank { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
