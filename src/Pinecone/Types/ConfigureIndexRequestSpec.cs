using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record ConfigureIndexRequestSpec
{
    [JsonPropertyName("pod")]
    public required ConfigureIndexRequestSpecPod Pod { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
