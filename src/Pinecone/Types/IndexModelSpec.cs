using System.Text.Json.Serialization;
using Pinecone;

#nullable enable

namespace Pinecone;

public record IndexModelSpec
{
    [JsonPropertyName("pod")]
    public PodSpec? Pod { get; set; }

    [JsonPropertyName("serverless")]
    public ServerlessSpec? Serverless { get; set; }
}
