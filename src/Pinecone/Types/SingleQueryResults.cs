using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record SingleQueryResults
{
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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the SingleQueryResults type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.SingleQueryResults ToProto()
    {
        var result = new Proto.SingleQueryResults();
        if (Matches != null && Matches.Any())
        {
            result.Matches.AddRange(Matches.Select(elem => elem.ToProto()));
        }
        if (Namespace != null)
        {
            result.Namespace = Namespace ?? "";
        }
        return result;
    }

    /// <summary>
    /// Returns a new SingleQueryResults type from its Protobuf-equivalent representation.
    /// </summary>
    internal static SingleQueryResults FromProto(Proto.SingleQueryResults value)
    {
        return new SingleQueryResults
        {
            Matches = value.Matches?.Select(ScoredVector.FromProto),
            Namespace = value.Namespace,
        };
    }
}
