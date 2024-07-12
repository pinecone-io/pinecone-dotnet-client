using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record ConfigureIndexRequest
{
    [JsonPropertyName("spec")]
    public required ConfigureIndexRequestSpec Spec { get; init; }
}
