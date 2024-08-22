using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record ListItem
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the ListItem type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.ListItem ToProto()
    {
        var result = new Proto.ListItem();
        if (Id != null)
        {
            result.Id = Id ?? "";
        }
        return result;
    }

    /// <summary>
    /// Returns a new ListItem type from its Protobuf-equivalent representation.
    /// </summary>
    internal static ListItem FromProto(Proto.ListItem value)
    {
        return new ListItem { Id = value.Id };
    }
}
