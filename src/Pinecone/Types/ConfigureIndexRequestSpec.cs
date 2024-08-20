using System.Text.Json.Serialization;
using Pinecone;

#nullable enable

namespace Pinecone;

public record ConfigureIndexRequestSpec
{
    [JsonPropertyName("pod")]
    public required ConfigureIndexRequestSpecPod Pod { get; set; }
}
