using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

public record Vector
{
    /// <summary>
    /// This is the vector's unique id.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// This is the vector data included in the request.
    /// </summary>
    [JsonPropertyName("values")]
    public ReadOnlyMemory<float>? Values { get; set; }

    /// <summary>
    /// This is the sparse data included in the request. Can only be specified if `sparse` index.
    /// </summary>
    [JsonPropertyName("sparseValues")]
    public SparseValues? SparseValues { get; set; }

    /// <summary>
    /// This is the metadata included in the request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Metadata? Metadata { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Returns a new Vector type from its Protobuf-equivalent representation.
    /// </summary>
    internal static Vector FromProto(ProtoGrpc.Vector value)
    {
        return new Vector
        {
            Id = value.Id,
            Values = value.Values?.ToArray(),
            SparseValues =
                value.SparseValues != null ? SparseValues.FromProto(value.SparseValues) : null,
            Metadata = value.Metadata != null ? Metadata.FromProto(value.Metadata) : null,
        };
    }

    /// <summary>
    /// Maps the Vector type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.Vector ToProto()
    {
        var result = new ProtoGrpc.Vector();
        result.Id = Id;
        if (Values != null && !Values.Value.IsEmpty)
        {
            result.Values.AddRange(Values.Value.ToArray());
        }
        if (SparseValues != null)
        {
            result.SparseValues = SparseValues.ToProto();
        }
        if (Metadata != null)
        {
            result.Metadata = Metadata.ToProto();
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
