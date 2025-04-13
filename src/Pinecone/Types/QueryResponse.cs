using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

/// <summary>
/// The response for the `query` operation. These are the matches found for a particular query vector. The matches are ordered from most similar to least similar.
/// </summary>
public record QueryResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    /// <summary>
    /// Returns a new QueryResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static QueryResponse FromProto(ProtoGrpc.QueryResponse value)
    {
        return new QueryResponse
        {
            Results = value.Results?.Select(SingleQueryResults.FromProto),
            Matches = value.Matches?.Select(ScoredVector.FromProto),
            Namespace = value.Namespace,
            Usage = value.Usage != null ? Usage.FromProto(value.Usage) : null,
        };
    }

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <summary>
    /// Maps the QueryResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.QueryResponse ToProto()
    {
        var result = new ProtoGrpc.QueryResponse();
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

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
