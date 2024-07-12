using System.Text.Json.Serialization;

#nullable enable

namespace Pinecone.Client;

public record EmbedRequestInputsItem
{
    [JsonPropertyName("text")]
    public string? Text { get; init; }
}
