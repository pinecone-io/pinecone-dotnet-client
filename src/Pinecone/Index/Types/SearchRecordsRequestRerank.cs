using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// Parameters for reranking the initial search results.
/// </summary>
public record SearchRecordsRequestRerank : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The name of the [reranking model](https://docs.pinecone.io/guides/inference/understanding-inference#reranking-models) to use.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// The field(s) to consider for reranking. If not provided, the default is `["text"]`.
    ///
    /// The number of fields supported is [model-specific](https://docs.pinecone.io/guides/inference/understanding-inference#reranking-models).
    /// </summary>
    [JsonPropertyName("rank_fields")]
    public IEnumerable<string> RankFields { get; set; } = new List<string>();

    /// <summary>
    /// The number of top results to return after reranking. Defaults to top_k.
    /// </summary>
    [JsonPropertyName("top_n")]
    public int? TopN { get; set; }

    /// <summary>
    /// Additional model-specific parameters. Refer to the [model guide](https://docs.pinecone.io/guides/inference/understanding-inference#reranking-models) for available model parameters.
    /// </summary>
    [JsonPropertyName("parameters")]
    public Dictionary<string, object?>? Parameters { get; set; }

    /// <summary>
    /// The query to rerank documents against. If a specific rerank query is specified,  it overwrites the query input that was provided at the top level.
    /// </summary>
    [JsonPropertyName("query")]
    public string? Query { get; set; }

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
