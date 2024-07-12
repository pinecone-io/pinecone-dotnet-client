using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record ServerlessSpec
{
    /// <summary>
    /// The public cloud where you would like your index hosted. Serverless indexes can be hosted only in AWS at this time.
    /// </summary>
    [JsonPropertyName("cloud")]
    public required ServerlessSpecCloud Cloud { get; init; }

    /// <summary>
    /// The region where you would like your index to be created. Serverless indexes can be created only in the us-east-1,us-west-2, and eu-west-1 regions of AWS at this time.
    /// </summary>
    [JsonPropertyName("region")]
    public required string Region { get; init; }
}
