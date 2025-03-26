using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

/// <summary>
/// The response for the `update` operation.
/// </summary>
public record UpdateResponse
{
    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Returns a new UpdateResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static UpdateResponse FromProto(ProtoGrpc.UpdateResponse value)
    {
        return new UpdateResponse();
    }

    /// <summary>
    /// Maps the UpdateResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.UpdateResponse ToProto()
    {
        return new ProtoGrpc.UpdateResponse();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
