using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record IndexList
{
    [JsonPropertyName("indexes")]
    public IEnumerable<Index>? Indexes { get; set; }
}
