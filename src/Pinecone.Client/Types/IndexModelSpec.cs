using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record IndexModelSpec
{
    [JsonPropertyName("pod")]
    public PodSpec? Pod { get; init; }

    [JsonPropertyName("serverless")]
    public ServerlessSpec? Serverless { get; init; }
}
