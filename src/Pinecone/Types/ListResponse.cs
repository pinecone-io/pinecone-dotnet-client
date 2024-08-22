using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record ListResponse
{
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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the ListResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.ListResponse ToProto()
    {
        var result = new Proto.ListResponse();
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

    /// <summary>
    /// Returns a new ListResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static ListResponse FromProto(Proto.ListResponse value)
    {
        return new ListResponse
        {
            Vectors = value.Vectors?.Select(ListItem.FromProto),
            Pagination = value.Pagination != null ? Pagination.FromProto(value.Pagination) : null,
            Namespace = value.Namespace,
            Usage = value.Usage != null ? Usage.FromProto(value.Usage) : null,
        };
    }
}
