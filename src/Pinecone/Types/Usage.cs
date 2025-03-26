using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

public record Usage
{
    /// <summary>
    /// The number of read units consumed by this operation.
    /// </summary>
    [JsonPropertyName("readUnits")]
    public uint? ReadUnits { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Returns a new Usage type from its Protobuf-equivalent representation.
    /// </summary>
    internal static Usage FromProto(ProtoGrpc.Usage value)
    {
        return new Usage { ReadUnits = value.ReadUnits };
    }

    /// <summary>
    /// Maps the Usage type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.Usage ToProto()
    {
        var result = new ProtoGrpc.Usage();
        if (ReadUnits != null)
        {
            result.ReadUnits = ReadUnits ?? 0;
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
