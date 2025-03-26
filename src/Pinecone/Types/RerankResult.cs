using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The result of a reranking request.
/// </summary>
public record RerankResult
{
    /// <summary>
    /// The model used to rerank documents.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// The reranked documents.
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<RankedDocument> Data { get; set; } = new List<RankedDocument>();

    /// <summary>
    /// Usage statistics for the model inference.
    /// </summary>
    [JsonPropertyName("usage")]
    public required RerankResultUsage Usage { get; set; }

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
