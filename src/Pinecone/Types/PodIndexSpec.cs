using System.Text.Json.Serialization;
using Pinecone;

#nullable enable

namespace Pinecone;

public record PodIndexSpec
{
    [JsonPropertyName("pod")]
    public PodSpec? Pod { get; set; }
}
