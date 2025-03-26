using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

/// <summary>
/// The response for the `upsert` operation.
/// </summary>
public record UpsertResponse
{
    /// <summary>
    /// The number of vectors upserted.
    /// </summary>
    [JsonPropertyName("upsertedCount")]
    public uint? UpsertedCount { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Returns a new UpsertResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static UpsertResponse FromProto(ProtoGrpc.UpsertResponse value)
    {
        return new UpsertResponse { UpsertedCount = value.UpsertedCount };
    }

    /// <summary>
    /// Maps the UpsertResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.UpsertResponse ToProto()
    {
        var result = new ProtoGrpc.UpsertResponse();
        if (UpsertedCount != null)
        {
            result.UpsertedCount = UpsertedCount ?? 0;
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
