using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record PodSpec
{
    /// <summary>
    /// The environment where the index is hosted.
    /// </summary>
    [JsonPropertyName("environment")]
    public required string Environment { get; set; }

    /// <summary>
    /// The number of replicas. Replicas duplicate your index. They provide higher availability and throughput. Replicas can be scaled up or down as your needs change.
    /// </summary>
    [JsonPropertyName("replicas")]
    public required int Replicas { get; set; }

    /// <summary>
    /// The number of shards. Shards split your data across multiple pods so you can fit more data into an index.
    /// </summary>
    [JsonPropertyName("shards")]
    public required int Shards { get; set; }

    /// <summary>
    /// The type of pod to use. One of `s1`, `p1`, or `p2` appended with `.` and one of `x1`, `x2`, `x4`, or `x8`.
    /// </summary>
    [JsonPropertyName("pod_type")]
    public required string PodType { get; set; }

    /// <summary>
    /// The number of pods to be used in the index. This should be equal to `shards` x `replicas`.'
    /// </summary>
    [JsonPropertyName("pods")]
    public required int Pods { get; set; }

    /// <summary>
    /// Configuration for the behavior of Pinecone's internal metadata index. By default, all metadata is indexed; when `metadata_config` is present, only specified metadata fields are indexed. These configurations are only valid for use with pod-based indexes.
    /// </summary>
    [JsonPropertyName("metadata_config")]
    public PodSpecMetadataConfig? MetadataConfig { get; set; }

    /// <summary>
    /// The name of the collection to be used as the source for the index.
    /// </summary>
    [JsonPropertyName("source_collection")]
    public string? SourceCollection { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
