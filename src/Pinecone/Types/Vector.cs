using System.Text.Json.Serialization;
using Google.Protobuf.Reflection;
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
    public IEnumerable<float> Values { get; set; } = new List<float>();

    [JsonPropertyName("sparseValues")]
    public SparseValues? SparseValues { get; set; }

    /// <summary>
    /// This is the metadata included in the request.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Metadata? Metadata { get; set; }

    #region Mappers

    public Proto.Vector ToProto()
    {
        var vector = new Proto.Vector { Id = Id, };
        if (Values.Any())
        {
            vector.Values.AddRange(Values);
        }
        if (SparseValues != null)
        {
            vector.SparseValues = SparseValues.ToProto();
        }
        if (Metadata != null)
        {
            vector.Metadata = Metadata.ToProto();
        }
        return vector;
    }

    public static Vector FromProto(Proto.Vector proto)
    {
        return new Vector
        {
            Id = proto.Id,
            Values = proto.Values?.ToList() ?? [],
            SparseValues =
                proto.SparseValues != null ? SparseValues.FromProto(proto.SparseValues) : null,
            Metadata = proto.Metadata != null ? Metadata.FromProto(proto.Metadata) : null
        };
    }

    #endregion
}
