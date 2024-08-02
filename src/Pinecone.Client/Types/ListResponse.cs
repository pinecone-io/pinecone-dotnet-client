using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone.Client;

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
    
    #region Mappers
    
    public static ListResponse FromProto(Proto.ListResponse proto)
    {
        return new ListResponse
        {
            Vectors = proto.Vectors.Select(ListItem.FromProto),
            Pagination = proto.Pagination != null ? Pagination.FromProto(proto.Pagination) : null,
            Namespace = proto.Namespace,
            Usage = proto.Usage != null ? Usage.FromProto(proto.Usage) : null
        };
    }
    
    #endregion
}
