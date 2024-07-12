using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record ServerlessIndexSpec
{
    [JsonPropertyName("serverless")]
    public ServerlessSpec? Serverless { get; init; }
}
