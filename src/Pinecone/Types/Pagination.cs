using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

public record Pagination : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("next")]
    public string? Next { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    /// <summary>
    /// Returns a new Pagination type from its Protobuf-equivalent representation.
    /// </summary>
    internal static Pagination FromProto(ProtoGrpc.Pagination value)
    {
        return new Pagination { Next = value.Next };
    }

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <summary>
    /// Maps the Pagination type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.Pagination ToProto()
    {
        var result = new ProtoGrpc.Pagination();
        if (Next != null)
        {
            result.Next = Next ?? "";
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
