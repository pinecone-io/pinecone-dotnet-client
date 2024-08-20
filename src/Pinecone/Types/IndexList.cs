using System.Text.Json.Serialization;
using Pinecone;

#nullable enable

namespace Pinecone;

public record IndexList
{
    [JsonPropertyName("indexes")]
    public IEnumerable<Index>? Indexes { get; set; }
}
