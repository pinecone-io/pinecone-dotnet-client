using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record IndexModelSpec
{
    [JsonPropertyName("pod")]
    public PodSpec? Pod { get; init; }

    [JsonPropertyName("serverless")]
    public ServerlessSpec? Serverless { get; init; }
}
