using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

/// <summary>
/// A single query vector within a `QueryRequest`.
/// </summary>
public record QueryVector
{
    /// <summary>
    /// The query vector. This should be the same length as the dimension of the index being queried. Each `query()` request can contain only one of the parameters `id` or `vector`.
    /// </summary>
    [JsonPropertyName("values")]
    public ReadOnlyMemory<float>? Values { get; set; }

    /// <summary>
    /// The query sparse values.
    /// </summary>
    [JsonPropertyName("sparseValues")]
    public SparseValues? SparseValues { get; set; }

    /// <summary>
    /// An override for the number of results to return for this query vector.
    /// </summary>
    [JsonPropertyName("topK")]
    public uint? TopK { get; set; }

    /// <summary>
    /// An override the namespace to search.
    /// </summary>
    [JsonPropertyName("namespace")]
    public string? Namespace { get; set; }

    /// <summary>
    /// An override for the metadata filter to apply. This replaces the request-level filter.
    /// </summary>
    [JsonPropertyName("filter")]
    public Metadata? Filter { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Returns a new QueryVector type from its Protobuf-equivalent representation.
    /// </summary>
    internal static QueryVector FromProto(ProtoGrpc.QueryVector value)
    {
        return new QueryVector
        {
            Values = value.Values?.ToArray(),
            SparseValues =
                value.SparseValues != null ? SparseValues.FromProto(value.SparseValues) : null,
            TopK = value.TopK,
            Namespace = value.Namespace,
            Filter = value.Filter != null ? Metadata.FromProto(value.Filter) : null,
        };
    }

    /// <summary>
    /// Maps the QueryVector type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.QueryVector ToProto()
    {
        var result = new ProtoGrpc.QueryVector();
        if (Values != null && !Values.Value.IsEmpty)
        {
            result.Values.AddRange(Values.Value.ToArray());
        }
        if (SparseValues != null)
        {
            result.SparseValues = SparseValues.ToProto();
        }
        if (TopK != null)
        {
            result.TopK = TopK ?? 0;
        }
        if (Namespace != null)
        {
            result.Namespace = Namespace ?? "";
        }
        if (Filter != null)
        {
            result.Filter = Filter.ToProto();
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
