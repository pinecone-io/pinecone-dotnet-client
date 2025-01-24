using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record RankedDocument
{
    /// <summary>
    /// The index position of the document from the original request.
    /// </summary>
    [JsonPropertyName("index")]
    public required int Index { get; set; }

    /// <summary>
    /// The relevance of the document to the query, normalized between 0 and 1, with scores closer to 1 indicating higher relevance.
    /// </summary>
    [JsonPropertyName("score")]
    public required double Score { get; set; }

    [JsonPropertyName("document")]
    public object? Document { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
