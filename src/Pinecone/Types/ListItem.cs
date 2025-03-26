using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

public record ListItem
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Returns a new ListItem type from its Protobuf-equivalent representation.
    /// </summary>
    internal static ListItem FromProto(ProtoGrpc.ListItem value)
    {
        return new ListItem { Id = value.Id };
    }

    /// <summary>
    /// Maps the ListItem type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.ListItem ToProto()
    {
        var result = new ProtoGrpc.ListItem();
        if (Id != null)
        {
            result.Id = Id ?? "";
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
