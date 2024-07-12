using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record Index
{
    /// <summary>
    /// The name of the index. Resource name must be 1-45 characters long, start and end with an alphanumeric character, and consist only of lower case alphanumeric characters or '-'.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// The dimensions of the vectors to be inserted in the index.
    /// </summary>
    [JsonPropertyName("dimension")]
    public required int Dimension { get; init; }

    /// <summary>
    /// The distance metric to be used for similarity search. You can use 'euclidean', 'cosine', or 'dotproduct'.
    /// </summary>
    [JsonPropertyName("metric")]
    public required IndexModelMetric Metric { get; init; }

    /// <summary>
    /// The URL address where the index is hosted.
    /// </summary>
    [JsonPropertyName("host")]
    public required string Host { get; init; }

    [JsonPropertyName("spec")]
    public required IndexModelSpec Spec { get; init; }

    [JsonPropertyName("status")]
    public required IndexModelStatus Status { get; init; }
}
