using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record ConfigureIndexRequest
{
    [JsonPropertyName("spec")]
    public ConfigureIndexRequestSpec? Spec { get; set; }

    [JsonPropertyName("deletion_protection")]
    public DeletionProtection? DeletionProtection { get; set; }
}
