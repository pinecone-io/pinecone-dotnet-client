using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

/// <summary>
/// The response for the `List` operation.
/// </summary>
public record ListResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// A list of ids
    /// </summary>
    [JsonPropertyName("vectors")]
    public IEnumerable<ListItem>? Vectors { get; set; }

    /// <summary>
    /// Pagination token to continue past this listing
    /// </summary>
    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; set; }

    /// <summary>
    /// The namespace of the vectors.
    /// </summary>
    [JsonPropertyName("namespace")]
    public string? Namespace { get; set; }

    /// <summary>
    /// The usage for this operation.
    /// </summary>
    [JsonPropertyName("usage")]
    public Usage? Usage { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    /// <summary>
    /// Returns a new ListResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static ListResponse FromProto(ProtoGrpc.ListResponse value)
    {
        return new ListResponse
        {
            Vectors = value.Vectors?.Select(ListItem.FromProto),
            Pagination = value.Pagination != null ? Pagination.FromProto(value.Pagination) : null,
            Namespace = value.Namespace,
            Usage = value.Usage != null ? Usage.FromProto(value.Usage) : null,
        };
    }

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <summary>
    /// Maps the ListResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.ListResponse ToProto()
    {
        var result = new ProtoGrpc.ListResponse();
        if (Vectors != null && Vectors.Any())
        {
            result.Vectors.AddRange(Vectors.Select(elem => elem.ToProto()));
        }
        if (Pagination != null)
        {
            result.Pagination = Pagination.ToProto();
        }
        if (Namespace != null)
        {
            result.Namespace = Namespace ?? "";
        }
        if (Usage != null)
        {
            result.Usage = Usage.ToProto();
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
