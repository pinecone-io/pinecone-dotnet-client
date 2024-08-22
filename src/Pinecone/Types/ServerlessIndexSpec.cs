using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record ServerlessIndexSpec
{
    [JsonPropertyName("serverless")]
    public required ServerlessSpec Serverless { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
