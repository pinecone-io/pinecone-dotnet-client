using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the DescribeIndexStatsResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.DescribeIndexStatsResponse ToProto()
    {
        var result = new Proto.DescribeIndexStatsResponse();
        if (Namespaces != null && Namespaces.Any())
        {
            foreach (var kvp in Namespaces)
            {
                result.Namespaces.Add(kvp.Key, kvp.Value.ToProto());
            }
            ;
        }
        if (Dimension != null)
        {
            result.Dimension = Dimension ?? 0;
        }
        if (IndexFullness != null)
        {
            result.IndexFullness = IndexFullness ?? 0.0f;
        }
        if (TotalVectorCount != null)
        {
            result.TotalVectorCount = TotalVectorCount ?? 0;
        }
        return result;
    }

    /// <summary>
    /// Returns a new DescribeIndexStatsResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static DescribeIndexStatsResponse FromProto(Proto.DescribeIndexStatsResponse value)
    {
        return new DescribeIndexStatsResponse
        {
            Namespaces = value.Namespaces?.ToDictionary(
                kvp => kvp.Key,
                kvp => NamespaceSummary.FromProto(kvp.Value)
            ),
            Dimension = value.Dimension,
            IndexFullness = value.IndexFullness,
            TotalVectorCount = value.TotalVectorCount,
        };
    }
}
