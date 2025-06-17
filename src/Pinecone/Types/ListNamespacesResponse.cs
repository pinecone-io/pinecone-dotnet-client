using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

/// <summary>
/// The response for the list namespace operation.
/// </summary>
[Serializable]
public record ListNamespacesResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The list of namespaces belonging to this index.
    /// </summary>
    [JsonPropertyName("namespaces")]
    public IEnumerable<NamespaceDescription>? Namespaces { get; set; }

    /// <summary>
    /// Pagination token to continue past this listing
    /// </summary>
    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    /// <summary>
    /// Returns a new ListNamespacesResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static ListNamespacesResponse FromProto(ProtoGrpc.ListNamespacesResponse value)
    {
        return new ListNamespacesResponse
        {
            Namespaces = value.Namespaces?.Select(NamespaceDescription.FromProto),
            Pagination = value.Pagination != null ? Pagination.FromProto(value.Pagination) : null,
        };
    }

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <summary>
    /// Maps the ListNamespacesResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.ListNamespacesResponse ToProto()
    {
        var result = new ProtoGrpc.ListNamespacesResponse();
        if (Namespaces != null && Namespaces.Any())
        {
            result.Namespaces.AddRange(Namespaces.Select(elem => elem.ToProto()));
        }
        if (Pagination != null)
        {
            result.Pagination = Pagination.ToProto();
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
