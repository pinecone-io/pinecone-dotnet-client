using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the QueryResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.QueryResponse ToProto()
    {
        var result = new Proto.QueryResponse();
        if (Results != null && Results.Any())
        {
            result.Results.AddRange(Results.Select(elem => elem.ToProto()));
        }
        if (Matches != null && Matches.Any())
        {
            result.Matches.AddRange(Matches.Select(elem => elem.ToProto()));
        }
        if (Namespace != null)
        {
            result.Namespace = Namespace ?? "";
        }
        if (Usage != null)
        {
            result.Usage = Usage.ToProto();
        }
        return result;
    }

    /// <summary>
    /// Returns a new QueryResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static QueryResponse FromProto(Proto.QueryResponse value)
    {
        return new QueryResponse
        {
            Results = value.Results?.Select(SingleQueryResults.FromProto),
            Matches = value.Matches?.Select(ScoredVector.FromProto),
            Namespace = value.Namespace,
            Usage = value.Usage != null ? Usage.FromProto(value.Usage) : null,
        };
    }
}
