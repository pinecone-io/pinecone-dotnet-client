using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record PodIndexSpec
{
    [JsonPropertyName("pod")]
    public PodSpec? Pod { get; init; }
}
