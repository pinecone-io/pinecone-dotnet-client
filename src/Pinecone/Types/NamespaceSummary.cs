using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record NamespaceSummary
{
    /// <summary>
    /// The number of vectors stored in this namespace. Note that updates to this field may lag behind updates to the
    /// underlying index and corresponding query results, etc.
    /// </summary>
    [JsonPropertyName("vectorCount")]
    public uint? VectorCount { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the NamespaceSummary type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.NamespaceSummary ToProto()
    {
        var result = new Proto.NamespaceSummary();
        if (VectorCount != null)
        {
            result.VectorCount = VectorCount ?? 0;
        }
        return result;
    }

    /// <summary>
    /// Returns a new NamespaceSummary type from its Protobuf-equivalent representation.
    /// </summary>
    internal static NamespaceSummary FromProto(Proto.NamespaceSummary value)
    {
        return new NamespaceSummary { VectorCount = value.VectorCount };
    }
}
