using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

/// <summary>
/// The response for the `describe_index_stats` operation.
/// </summary>
public record DescribeIndexStatsResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// A mapping for each namespace in the index from the namespace name to a
    ///  summary of its contents. If a metadata filter expression is present, the
    ///  summary will reflect only vectors matching that expression.
    /// </summary>
    [JsonPropertyName("namespaces")]
    public Dictionary<string, NamespaceSummary>? Namespaces { get; set; }

    /// <summary>
    /// The dimension of the indexed vectors. Not specified if `sparse` index.
    /// </summary>
    [JsonPropertyName("dimension")]
    public uint? Dimension { get; set; }

    /// <summary>
    /// The fullness of the index, regardless of whether a metadata filter expression was passed. The granularity of this metric is 10%.
    ///
    ///  Serverless indexes scale automatically as needed, so index fullness is relevant only for pod-based indexes.
    ///
    ///  The index fullness result may be inaccurate during pod resizing; to get the status of a pod resizing process, use [`describe_index`](https://docs.pinecone.io/reference/api/2024-04/control-plane/describe_index).
    /// </summary>
    [JsonPropertyName("indexFullness")]
    public float? IndexFullness { get; set; }

    /// <summary>
    /// The total number of vectors in the index, regardless of whether a metadata filter expression was passed
    /// </summary>
    [JsonPropertyName("totalVectorCount")]
    public uint? TotalVectorCount { get; set; }

    /// <summary>
    /// The metric of the index.
    /// </summary>
    [JsonPropertyName("metric")]
    public string? Metric { get; set; }

    /// <summary>
    /// The type of the vector the index supports.
    /// </summary>
    [JsonPropertyName("vectorType")]
    public string? VectorType { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    /// <summary>
    /// Returns a new DescribeIndexStatsResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static DescribeIndexStatsResponse FromProto(ProtoGrpc.DescribeIndexStatsResponse value)
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
            Metric = value.Metric,
            VectorType = value.VectorType,
        };
    }

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <summary>
    /// Maps the DescribeIndexStatsResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.DescribeIndexStatsResponse ToProto()
    {
        var result = new ProtoGrpc.DescribeIndexStatsResponse();
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
        if (Metric != null)
        {
            result.Metric = Metric ?? "";
        }
        if (VectorType != null)
        {
            result.VectorType = VectorType ?? "";
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
