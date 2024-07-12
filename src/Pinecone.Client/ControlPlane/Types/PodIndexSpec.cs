using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record PodIndexSpec
{
    [JsonPropertyName("pod")]
    public PodSpec? Pod { get; init; }
}
