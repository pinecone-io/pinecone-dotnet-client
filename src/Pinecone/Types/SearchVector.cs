using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record SearchVector
{
    [JsonPropertyName("values")]
    public ReadOnlyMemory<float>? Values { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
