using System.Text.Json.Serialization;
using Pinecone;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record FetchResponse
{
    /// <summary>
    /// The fetched vectors, in the form of a map between the fetched ids and the fetched vectors
    /// </summary>
    [JsonPropertyName("vectors")]
    public Dictionary<string, Vector>? Vectors { get; set; }

    /// <summary>
    /// The namespace of the vectors.
    /// </summary>
    [JsonPropertyName("namespace")]
    public string? Namespace { get; set; }

    /// <summary>
    /// The usage for this operation.
    /// </summary>
    [JsonPropertyName("usage")]
    public Usage? Usage { get; set; }

    #region Mappers

    public static FetchResponse FromProto(Proto.FetchResponse proto)
    {
        return new FetchResponse
        {
            Vectors = proto.Vectors?.ToDictionary(
                kvp => kvp.Key,
                kvp => Vector.FromProto(kvp.Value)
            ),
            Namespace = proto.Namespace,
            Usage = proto.Usage != null ? Usage.FromProto(proto.Usage) : null
        };
    }

    #endregion
}
