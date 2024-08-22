using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record ServerlessSpec
{
    /// <summary>
    /// The public cloud where you would like your index hosted.
    /// </summary>
    [JsonPropertyName("cloud")]
    public required ServerlessSpecCloud Cloud { get; set; }

    /// <summary>
    /// The region where you would like your index to be created.
    /// </summary>
    [JsonPropertyName("region")]
    public required string Region { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
