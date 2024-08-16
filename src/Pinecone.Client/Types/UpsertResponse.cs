using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone.Client;

public record UpsertResponse
{
    /// <summary>
    /// The number of vectors upserted.
    /// </summary>
    [JsonPropertyName("upsertedCount")]
    public uint? UpsertedCount { get; set; }

    #region Mappers

    public static UpsertResponse FromProto(Proto.UpsertResponse proto)
    {
        return new UpsertResponse { UpsertedCount = proto.UpsertedCount };
    }

    #endregion
}
