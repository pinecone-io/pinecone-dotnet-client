using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

/// <summary>
/// The response for the `Delete` operation.
/// </summary>
public record DeleteResponse
{
    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Returns a new DeleteResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static DeleteResponse FromProto(ProtoGrpc.DeleteResponse value)
    {
        return new DeleteResponse();
    }

    /// <summary>
    /// Maps the DeleteResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.DeleteResponse ToProto()
    {
        return new ProtoGrpc.DeleteResponse();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
