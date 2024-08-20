using System.Text.Json.Serialization;
using Pinecone;

#nullable enable

namespace Pinecone;

public record CollectionList
{
    [JsonPropertyName("collections")]
    public IEnumerable<CollectionModel>? Collections { get; set; }
}
