using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

[Serializable]
public record ListItem : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    /// <summary>
    /// Returns a new ListItem type from its Protobuf-equivalent representation.
    /// </summary>
    internal static ListItem FromProto(ProtoGrpc.ListItem value)
    {
        return new ListItem { Id = value.Id };
    }

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

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
