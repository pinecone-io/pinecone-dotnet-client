using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

namespace Pinecone;

public record QueryRequest
{
    /// <summary>
    /// The namespace to query.
    /// </summary>
    [JsonPropertyName("namespace")]
    public string? Namespace { get; set; }

    /// <summary>
    /// The number of results to return for each query.
    /// </summary>
    [JsonPropertyName("topK")]
    public required uint TopK { get; set; }

    /// <summary>
    /// The filter to apply. You can use vector metadata to limit your search. See [Understanding metadata](https://docs.pinecone.io/guides/data/understanding-metadata).
    /// </summary>
    [JsonPropertyName("filter")]
    public Metadata? Filter { get; set; }

    /// <summary>
    /// Indicates whether vector values are included in the response.
    /// </summary>
    [JsonPropertyName("includeValues")]
    public bool? IncludeValues { get; set; }

    /// <summary>
    /// Indicates whether metadata is included in the response as well as the ids.
    /// </summary>
    [JsonPropertyName("includeMetadata")]
    public bool? IncludeMetadata { get; set; }

    /// <summary>
    /// DEPRECATED. The query vectors. Each `query()` request can contain only one of the parameters `queries`, `vector`, or  `id`.
    /// </summary>
    [JsonPropertyName("queries")]
    public IEnumerable<QueryVector>? Queries { get; set; }

    /// <summary>
    /// The query vector. This should be the same length as the dimension of the index being queried. Each `query()` request can contain only one of the parameters `id` or `vector`.
    /// </summary>
    [JsonPropertyName("vector")]
    public ReadOnlyMemory<float>? Vector { get; set; }

    /// <summary>
    /// The query sparse values.
    /// </summary>
    [JsonPropertyName("sparseVector")]
    public SparseValues? SparseVector { get; set; }

    /// <summary>
    /// The unique ID of the vector to be used as a query vector. Each `query()` request can contain only one of the parameters `queries`, `vector`, or  `id`.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Maps the QueryRequest type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.QueryRequest ToProto()
    {
        var result = new Proto.QueryRequest();
        if (Namespace != null)
        {
            result.Namespace = Namespace ?? "";
        }
        result.TopK = TopK;
        if (Filter != null)
        {
            result.Filter = Filter.ToProto();
        }
        if (IncludeValues != null)
        {
            result.IncludeValues = IncludeValues ?? false;
        }
        if (IncludeMetadata != null)
        {
            result.IncludeMetadata = IncludeMetadata ?? false;
        }
        if (Queries != null && Queries.Any())
        {
            result.Queries.AddRange(Queries.Select(elem => elem.ToProto()));
        }
        if (Vector != null && !Vector.Value.IsEmpty)
        {
            result.Vector.AddRange(Vector.Value.ToArray());
        }
        if (SparseVector != null)
        {
            result.SparseVector = SparseVector.ToProto();
        }
        if (Id != null)
        {
            result.Id = Id ?? "";
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
