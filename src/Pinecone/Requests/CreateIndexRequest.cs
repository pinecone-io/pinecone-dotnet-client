using System.Text.Json.Serialization;
using OneOf;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record CreateIndexRequest
{
    /// <summary>
    /// The name of the index. Resource name must be 1-45 characters long, start and end with an alphanumeric character, and consist only of lower case alphanumeric characters or '-'.
    ///
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// The dimensions of the vectors to be inserted in the index.
    /// </summary>
    [JsonPropertyName("dimension")]
    public required int Dimension { get; set; }

    /// <summary>
    /// The distance metric to be used for similarity search. You can use 'euclidean', 'cosine', or 'dotproduct'.
    /// </summary>
    [JsonPropertyName("metric")]
    public CreateIndexRequestMetric? Metric { get; set; }

    [JsonPropertyName("deletion_protection")]
    public DeletionProtection? DeletionProtection { get; set; }

    [JsonPropertyName("spec")]
    [JsonConverter(typeof(OneOfSerializer<OneOf<ServerlessIndexSpec, PodIndexSpec>>))]
    public required OneOf<ServerlessIndexSpec, PodIndexSpec> Spec { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
