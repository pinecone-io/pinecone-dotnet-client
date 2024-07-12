using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record CollectionList
{
    [JsonPropertyName("collections")]
    public IEnumerable<CollectionModel>? Collections { get; init; }
}
