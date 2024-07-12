using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record EmbedRequest
{
    [JsonPropertyName("model")]
    public required string Model { get; init; }

    /// <summary>
    /// Model-specific parameters.
    /// </summary>
    [JsonPropertyName("parameters")]
    public EmbedRequestParameters? Parameters { get; init; }

    [JsonPropertyName("inputs")]
    public IEnumerable<EmbedRequestInputsItem> Inputs { get; init; } =
        new List<EmbedRequestInputsItem>();
}
