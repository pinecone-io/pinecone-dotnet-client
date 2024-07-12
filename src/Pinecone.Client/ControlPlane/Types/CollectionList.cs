using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record CollectionList
{
    [JsonPropertyName("collections")]
    public IEnumerable<CollectionModel>? Collections { get; init; }
}
