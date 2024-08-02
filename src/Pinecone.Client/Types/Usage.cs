using System.Text.Json.Serialization;
using Google.Protobuf.Reflection;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone.Client;

public record Usage
{
    /// <summary>
    /// The number of read units consumed by this operation.
    /// </summary>
    [JsonPropertyName("readUnits")]
    public uint? ReadUnits { get; set; }
    
    #region Mappers

    public Proto.Usage ToProto()
    {
        var usage = new Proto.Usage();
        if (ReadUnits != null)
        {
            usage.ReadUnits = ReadUnits ?? 0;
        }
        return usage;
    }

    public static Usage FromProto(Proto.Usage proto)
    {
        return new Usage
        {
            ReadUnits = proto.ReadUnits
        };
    }

    #endregion
}
