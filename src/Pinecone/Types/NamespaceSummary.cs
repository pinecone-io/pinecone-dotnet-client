using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

/// <summary>
/// A summary of the contents of a namespace.
/// </summary>
[Serializable]
public record NamespaceSummary : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The number of vectors stored in this namespace. Note that updates to this field may lag behind updates to the
    ///  underlying index and corresponding query results, etc.
    /// </summary>
    [JsonPropertyName("vectorCount")]
    public uint? VectorCount { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    /// <summary>
    /// Returns a new NamespaceSummary type from its Protobuf-equivalent representation.
    /// </summary>
    internal static NamespaceSummary FromProto(ProtoGrpc.NamespaceSummary value)
    {
        return new NamespaceSummary { VectorCount = value.VectorCount };
    }

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <summary>
    /// Maps the NamespaceSummary type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.NamespaceSummary ToProto()
    {
        var result = new ProtoGrpc.NamespaceSummary();
        if (VectorCount != null)
        {
            result.VectorCount = VectorCount ?? 0;
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
