using System.Text.Json.Serialization;
using Google.Protobuf.Reflection;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone.Client;

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
    /// The filter to apply. You can use vector metadata to limit your search. See [Filter with metadata](https://docs.pinecone.io/guides/data/filter-with-metadata).
    /// </summary>
    [JsonPropertyName("filter")]
    public Dictionary<string, MetadataValue?>? Filter { get; set; }

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
    public IEnumerable<float>? Vector { get; set; }

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

    #region Mappers

    public Proto.QueryRequest ToProto()
    {
        var queryRequest = new Proto.QueryRequest { TopK = TopK, };
        if (Namespace != null)
        {
            queryRequest.Namespace = Namespace;
        }
        if (Filter != null)
        {
            queryRequest.Filter = Core.ProtoConverter.ToProtoStruct(Filter);
        }
        if (IncludeValues != null)
        {
            queryRequest.IncludeValues = IncludeValues ?? false;
        }
        if (IncludeMetadata != null)
        {
            queryRequest.IncludeMetadata = IncludeMetadata ?? false;
        }
        if (Queries != null && Queries.Any())
        {
            queryRequest.Queries.AddRange(Queries.Select(elem => elem.ToProto()));
        }
        if (Vector != null)
        {
            queryRequest.Vector.AddRange(Vector);
        }
        if (SparseVector != null)
        {
            queryRequest.SparseVector = SparseVector.ToProto();
        }
        if (Id != null)
        {
            queryRequest.Id = Id;
        }
        return queryRequest;
    }

    #endregion
}
