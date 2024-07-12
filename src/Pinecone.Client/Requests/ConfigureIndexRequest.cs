using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record ConfigureIndexRequest
{
    [JsonPropertyName("spec")]
    public required ConfigureIndexRequestSpec Spec { get; init; }
}
