using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
