using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record RerankResultUsage
{
    [JsonPropertyName("rerank_units")]
    public int? RerankUnits { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
