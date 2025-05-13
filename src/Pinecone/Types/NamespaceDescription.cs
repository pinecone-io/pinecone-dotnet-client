using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

public record NamespaceDescription : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("recordCount")]
    public ulong? RecordCount { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    /// <summary>
    /// Returns a new NamespaceDescription type from its Protobuf-equivalent representation.
    /// </summary>
    internal static NamespaceDescription FromProto(ProtoGrpc.NamespaceDescription value)
    {
        return new NamespaceDescription { Name = value.Name, RecordCount = value.RecordCount };
    }

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <summary>
    /// Maps the NamespaceDescription type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.NamespaceDescription ToProto()
    {
        var result = new ProtoGrpc.NamespaceDescription();
        if (Name != null)
        {
            result.Name = Name ?? "";
        }
        if (RecordCount != null)
        {
            result.RecordCount = RecordCount ?? 0;
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
