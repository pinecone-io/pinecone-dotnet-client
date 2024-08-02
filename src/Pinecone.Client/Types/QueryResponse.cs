using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone.Client;

public record QueryResponse
{
    /// <summary>
    /// DEPRECATED. The results of each query. The order is the same as `QueryRequest.queries`.
    /// </summary>
    [JsonPropertyName("results")]
    public IEnumerable<SingleQueryResults>? Results { get; set; }

    /// <summary>
    /// The matches for the vectors.
    /// </summary>
    [JsonPropertyName("matches")]
    public IEnumerable<ScoredVector>? Matches { get; set; }

    /// <summary>
    /// The namespace for the vectors.
    /// </summary>
    [JsonPropertyName("namespace")]
    public string? Namespace { get; set; }

    /// <summary>
    /// The usage for this operation.
    /// </summary>
    [JsonPropertyName("usage")]
    public Usage? Usage { get; set; }
    
    #region Mappers

    public static QueryResponse FromProto(Proto.QueryResponse proto)
    {
        return new QueryResponse
        {
            Results = proto.Results.Select(SingleQueryResults.FromProto),
            Matches = proto.Matches.Select(ScoredVector.FromProto),
            Namespace = proto.Namespace,
            Usage = proto.Usage != null ? Usage.FromProto(proto.Usage) : null
        };
    }

    #endregion
}
