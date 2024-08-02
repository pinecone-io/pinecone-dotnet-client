using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record ConfigureIndexRequestSpec
{
    [JsonPropertyName("pod")]
    public required ConfigureIndexRequestSpecPod Pod { get; set; }
}
