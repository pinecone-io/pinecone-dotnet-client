using System.Data;
using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone.Client;

public record QueryVector
{
    /// <summary>
    /// The query vector values. This should be the same length as the dimension of the index being queried.
    /// </summary>
    [JsonPropertyName("values")]
    public IEnumerable<float> Values { get; set; } = new List<float>();

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
    public Dictionary<string, MetadataValue?>? Filter { get; set; }
    
    #region Mappers
    
    public Proto.QueryVector ToProto()
    {
        var queryVector = new Proto.QueryVector();
        if (Values.Any())
        {
            queryVector.Values.AddRange(Values);
        }
        if (SparseValues != null)
        {
            queryVector.SparseValues = SparseValues.ToProto();
        }
        if (TopK != null)
        {
            queryVector.TopK = TopK ?? 0;
        }
        if (Namespace != null)
        {
            queryVector.Namespace = Namespace ?? "";
        }
        if (Filter != null)
        {
            queryVector.Filter = Core.ProtoConverter.ToProtoStruct(Filter);
        }
        return queryVector;
    } 
    
    #endregion
}
