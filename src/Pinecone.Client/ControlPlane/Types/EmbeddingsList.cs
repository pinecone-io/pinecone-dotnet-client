using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record EmbeddingsList
{
    [JsonPropertyName("model")]
    public string? Model { get; init; }

    [JsonPropertyName("data")]
    public IEnumerable<Embedding>? Data { get; init; }

    /// <summary>
    /// Usage statistics for model inference including any instruction prefixes
    /// </summary>
    [JsonPropertyName("usage")]
    public EmbeddingsListUsage? Usage { get; init; }
}
