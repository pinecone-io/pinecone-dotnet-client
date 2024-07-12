using System.Text.Json.Serialization;

#nullable enable

namespace Pinecone.Client;

public record EmbeddingsListUsage
{
    [JsonPropertyName("total_tokens")]
    public int? TotalTokens { get; init; }
}
