using System.Text.Json.Serialization;
using Pinecone;

#nullable enable

namespace Pinecone;

public record ServerlessIndexSpec
{
    [JsonPropertyName("serverless")]
    public ServerlessSpec? Serverless { get; set; }
}
