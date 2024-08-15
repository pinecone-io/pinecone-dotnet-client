using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone.Client;

public record DescribeIndexStatsResponse
{
    /// <summary>
    /// A mapping for each namespace in the index from the namespace name to a
    /// summary of its contents. If a metadata filter expression is present, the
    /// summary will reflect only vectors matching that expression.
    /// </summary>
    [JsonPropertyName("namespaces")]
    public Dictionary<string, NamespaceSummary>? Namespaces { get; set; }

    /// <summary>
    /// The dimension of the indexed vectors.
    /// </summary>
    [JsonPropertyName("dimension")]
    public uint? Dimension { get; set; }

    /// <summary>
    /// The fullness of the index, regardless of whether a metadata filter expression was passed. The granularity of this metric is 10%.
    ///
    /// Serverless indexes scale automatically as needed, so index fullness is relevant only for pod-based indexes.
    ///
    /// The index fullness result may be inaccurate during pod resizing; to get the status of a pod resizing process, use [`describe_index`](https://docs.pinecone.io/reference/api/control-plane/describe_index).
    /// </summary>
    [JsonPropertyName("indexFullness")]
    public float? IndexFullness { get; set; }

    /// <summary>
    /// The total number of vectors in the index, regardless of whether a metadata filter expression was passed
    /// </summary>
    [JsonPropertyName("totalVectorCount")]
    public uint? TotalVectorCount { get; set; }
    
    #region Mappers
    
    public static DescribeIndexStatsResponse FromProto(Proto.DescribeIndexStatsResponse proto)
    {
        return new DescribeIndexStatsResponse
        {
            Namespaces = proto.Namespaces?.ToDictionary(
                kvp => kvp.Key,
                kvp => NamespaceSummary.FromProto(kvp.Value)
            ),
            Dimension = proto.Dimension,
            IndexFullness = proto.IndexFullness,
            TotalVectorCount = proto.TotalVectorCount,
        };
    }

    #endregion
}
