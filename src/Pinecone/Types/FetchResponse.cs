using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

/// <summary>
/// The response for the `fetch` operation.
/// </summary>
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

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Returns a new FetchResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static FetchResponse FromProto(ProtoGrpc.FetchResponse value)
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

    /// <summary>
    /// Maps the FetchResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.FetchResponse ToProto()
    {
        var result = new ProtoGrpc.FetchResponse();
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

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
