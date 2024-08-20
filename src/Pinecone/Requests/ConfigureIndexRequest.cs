using System.Text.Json.Serialization;
using Pinecone;

#nullable enable

namespace Pinecone;

public record ConfigureIndexRequest
{
    [JsonPropertyName("spec")]
    public ConfigureIndexRequestSpec? Spec { get; set; }

    [JsonPropertyName("deletion_protection")]
    public DeletionProtection? DeletionProtection { get; set; }
}
