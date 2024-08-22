using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record CollectionList
{
    [JsonPropertyName("collections")]
    public IEnumerable<CollectionModel>? Collections { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
