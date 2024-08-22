using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record UpsertResponse
{
    /// <summary>
    /// The number of vectors upserted.
    /// </summary>
    [JsonPropertyName("upsertedCount")]
    public uint? UpsertedCount { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the UpsertResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.UpsertResponse ToProto()
    {
        var result = new Proto.UpsertResponse();
        if (UpsertedCount != null)
        {
            result.UpsertedCount = UpsertedCount ?? 0;
        }
        return result;
    }

    /// <summary>
    /// Returns a new UpsertResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static UpsertResponse FromProto(Proto.UpsertResponse value)
    {
        return new UpsertResponse { UpsertedCount = value.UpsertedCount };
    }
}
