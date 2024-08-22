using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record IndexList
{
    [JsonPropertyName("indexes")]
    public IEnumerable<Index>? Indexes { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
