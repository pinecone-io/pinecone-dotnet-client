using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record RankedDocument
{
    /// <summary>
    /// The index of the document
    /// </summary>
    [JsonPropertyName("index")]
    public required int Index { get; set; }

    /// <summary>
    /// The relevance score of the document normalized between 0 and 1.
    /// </summary>
    [JsonPropertyName("score")]
    public required double Score { get; set; }

    [JsonPropertyName("document")]
    public Dictionary<string, string>? Document { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
