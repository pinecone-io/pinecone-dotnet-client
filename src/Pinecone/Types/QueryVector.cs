using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record QueryVector
{
    /// <summary>
    /// The query vector values. This should be the same length as the dimension of the index being queried.
    /// </summary>
    [JsonPropertyName("values")]
    public ReadOnlyMemory<float> Values { get; set; }

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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the QueryVector type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.QueryVector ToProto()
    {
        var result = new Proto.QueryVector();
        if (!Values.IsEmpty)
        {
            result.Values.AddRange(Values.ToArray());
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

    /// <summary>
    /// Returns a new QueryVector type from its Protobuf-equivalent representation.
    /// </summary>
    internal static QueryVector FromProto(Proto.QueryVector value)
    {
        return new QueryVector
        {
            Values = value.Values?.ToArray() ?? new ReadOnlyMemory<float>(),
            SparseValues =
                value.SparseValues != null ? SparseValues.FromProto(value.SparseValues) : null,
            TopK = value.TopK,
            Namespace = value.Namespace,
            Filter = value.Filter != null ? Metadata.FromProto(value.Filter) : null,
        };
    }
}
