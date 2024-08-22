using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record Usage
{
    /// <summary>
    /// The number of read units consumed by this operation.
    /// </summary>
    [JsonPropertyName("readUnits")]
    public uint? ReadUnits { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the Usage type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.Usage ToProto()
    {
        var result = new Proto.Usage();
        if (ReadUnits != null)
        {
            result.ReadUnits = ReadUnits ?? 0;
        }
        return result;
    }

    /// <summary>
    /// Returns a new Usage type from its Protobuf-equivalent representation.
    /// </summary>
    internal static Usage FromProto(Proto.Usage value)
    {
        return new Usage { ReadUnits = value.ReadUnits };
    }
}
