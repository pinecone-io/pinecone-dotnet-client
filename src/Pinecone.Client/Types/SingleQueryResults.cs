using System.Text.Json.Serialization;
using Pinecone.Client;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone.Client;

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

    #region Mappers

    public static SingleQueryResults FromProto(Proto.SingleQueryResults proto)
    {
        return new SingleQueryResults
        {
            Matches = proto.Matches?.Select(ScoredVector.FromProto),
            Namespace = proto.Namespace,
        };
    }

    #endregion
}
