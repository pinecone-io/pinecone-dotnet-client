using System.Text.Json.Serialization;
using Pinecone.Core;
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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the FetchResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.FetchResponse ToProto()
    {
        var result = new Proto.FetchResponse();
        if (Vectors != null && Vectors.Any())
        {
            foreach (var kvp in Vectors)
            {
                result.Vectors.Add(kvp.Key, kvp.Value.ToProto());
            }
            ;
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
    /// Returns a new FetchResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static FetchResponse FromProto(Proto.FetchResponse value)
    {
        return new FetchResponse
        {
            Vectors = value.Vectors?.ToDictionary(
                kvp => kvp.Key,
                kvp => Vector.FromProto(kvp.Value)
            ),
            Namespace = value.Namespace,
            Usage = value.Usage != null ? Usage.FromProto(value.Usage) : null,
        };
    }
}
