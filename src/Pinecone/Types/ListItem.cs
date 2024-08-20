using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record ListItem
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    #region Mappers

    public static ListItem FromProto(Proto.ListItem proto)
    {
        return new ListItem { Id = proto.Id, };
    }

    #endregion
}
