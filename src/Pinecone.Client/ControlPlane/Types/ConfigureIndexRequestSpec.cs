using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record ConfigureIndexRequestSpec
{
    [JsonPropertyName("pod")]
    public required ConfigureIndexRequestSpecPod Pod { get; init; }
}
