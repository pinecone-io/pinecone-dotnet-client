using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

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
    public ReadOnlyMemory<float> Values { get; set; }

    [JsonPropertyName("sparseValues")]
    public SparseValues? SparseValues { get; set; }

    /// <summary>
    /// This is the metadata included in the request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Metadata? Metadata { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the Vector type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.Vector ToProto()
    {
        var result = new Proto.Vector();
        result.Id = Id;
        if (!Values.IsEmpty)
        {
            result.Values.AddRange(Values.ToArray());
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

    /// <summary>
    /// Returns a new Vector type from its Protobuf-equivalent representation.
    /// </summary>
    internal static Vector FromProto(Proto.Vector value)
    {
        return new Vector
        {
            Id = value.Id,
            Values = value.Values?.ToArray() ?? new ReadOnlyMemory<float>(),
            SparseValues =
                value.SparseValues != null ? SparseValues.FromProto(value.SparseValues) : null,
            Metadata = value.Metadata != null ? Metadata.FromProto(value.Metadata) : null,
        };
    }
}
