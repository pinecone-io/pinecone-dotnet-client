using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

namespace Pinecone;

public record ListRequest
{
    /// <summary>
    /// The vector IDs to fetch. Does not accept values containing spaces.
    /// </summary>
    [JsonIgnore]
    public string? Prefix { get; set; }

    /// <summary>
    /// Max number of ids to return
    /// </summary>
    [JsonIgnore]
    public uint? Limit { get; set; }

    /// <summary>
    /// Pagination token to continue a previous listing operation
    /// </summary>
    [JsonIgnore]
    public string? PaginationToken { get; set; }

    [JsonIgnore]
    public string? Namespace { get; set; }

    /// <summary>
    /// Maps the ListRequest type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.ListRequest ToProto()
    {
        var result = new Proto.ListRequest();
        if (Prefix != null)
        {
            result.Prefix = Prefix ?? "";
        }
        if (Limit != null)
        {
            result.Limit = Limit ?? 0;
        }
        if (PaginationToken != null)
        {
            result.PaginationToken = PaginationToken ?? "";
        }
        if (Namespace != null)
        {
            result.Namespace = Namespace ?? "";
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
