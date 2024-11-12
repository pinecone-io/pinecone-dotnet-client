using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record RerankRequest
{
    /// <summary>
    /// The [model](https://docs.pinecone.io/guides/inference/understanding-inference#models) to use for reranking.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// The query to rerank documents against.
    /// </summary>
    [JsonPropertyName("query")]
    public required string Query { get; set; }

    /// <summary>
    /// The number of results to return sorted by relevance. Defaults to the number of inputs.
    /// </summary>
    [JsonPropertyName("top_n")]
    public int? TopN { get; set; }

    /// <summary>
    /// Whether to return the documents in the response.
    /// </summary>
    [JsonPropertyName("return_documents")]
    public bool? ReturnDocuments { get; set; }

    /// <summary>
    /// The fields to rank the documents by. If not provided, the default is `"text"`.
    /// </summary>
    [JsonPropertyName("rank_fields")]
    public IEnumerable<string>? RankFields { get; set; }

    /// <summary>
    /// The documents to rerank.
    /// </summary>
    [JsonPropertyName("documents")]
    public IEnumerable<Dictionary<string, string>> Documents { get; set; } =
        new List<Dictionary<string, string>>();

    /// <summary>
    /// Additional model-specific parameters for the reranker.
    /// </summary>
    [JsonPropertyName("parameters")]
    public Dictionary<string, string>? Parameters { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}