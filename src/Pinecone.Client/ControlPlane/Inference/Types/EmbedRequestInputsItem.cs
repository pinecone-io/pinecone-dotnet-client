using System.Text.Json.Serialization;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record EmbedRequestInputsItem
{
    [JsonPropertyName("text")]
    public string? Text { get; init; }
}
