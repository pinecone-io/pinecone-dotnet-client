using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record IndexList
{
    [JsonPropertyName("indexes")]
    public IEnumerable<Index>? Indexes { get; init; }
}
