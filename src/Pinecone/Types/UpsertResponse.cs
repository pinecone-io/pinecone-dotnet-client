using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

/// <summary>
/// The response for the `upsert` operation.
/// </summary>
public record UpsertResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The number of vectors upserted.
    /// </summary>
    [JsonPropertyName("upsertedCount")]
    public uint? UpsertedCount { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    /// <summary>
    /// Returns a new UpsertResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static UpsertResponse FromProto(ProtoGrpc.UpsertResponse value)
    {
        return new UpsertResponse { UpsertedCount = value.UpsertedCount };
    }

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <summary>
    /// Maps the UpsertResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.UpsertResponse ToProto()
    {
        var result = new ProtoGrpc.UpsertResponse();
        if (UpsertedCount != null)
        {
            result.UpsertedCount = UpsertedCount ?? 0;
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
