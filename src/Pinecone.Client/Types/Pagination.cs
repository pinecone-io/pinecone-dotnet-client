using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone.Client;

public record Pagination
{
    [JsonPropertyName("next")]
    public string? Next { get; set; }

    #region Mappers

    public static Pagination FromProto(Proto.Pagination proto)
    {
        return new Pagination { Next = proto.Next, };
    }

    #endregion
}
