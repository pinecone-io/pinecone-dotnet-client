using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record PodIndexSpec
{
    [JsonPropertyName("pod")]
    public required PodSpec Pod { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
