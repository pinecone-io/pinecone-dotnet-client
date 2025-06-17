using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

[Serializable]
public record RerankRequest
{
    /// <summary>
    /// The [model](https://docs.pinecone.io/guides/search/rerank-results#reranking-models) to use for reranking.
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
    /// The field(s) to consider for reranking. If not provided, the default is `["text"]`.
    ///
    /// The number of fields supported is [model-specific](https://docs.pinecone.io/guides/search/rerank-results#reranking-models).
    /// </summary>
    [JsonPropertyName("rank_fields")]
    public IEnumerable<string>? RankFields { get; set; }

    /// <summary>
    /// The documents to rerank.
    /// </summary>
    [JsonPropertyName("documents")]
    public IEnumerable<Dictionary<string, object?>> Documents { get; set; } =
        new List<Dictionary<string, object?>>();

    /// <summary>
    /// Additional model-specific parameters. Refer to the [model guide](https://docs.pinecone.io/guides/search/rerank-results#reranking-models) for available model parameters.
    /// </summary>
    [JsonPropertyName("parameters")]
    public Dictionary<string, object?>? Parameters { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
