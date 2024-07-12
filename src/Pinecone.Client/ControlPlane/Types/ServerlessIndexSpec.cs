using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record ServerlessIndexSpec
{
    [JsonPropertyName("serverless")]
    public ServerlessSpec? Serverless { get; init; }
}
