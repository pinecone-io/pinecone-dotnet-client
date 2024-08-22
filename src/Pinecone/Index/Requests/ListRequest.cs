using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record ListRequest
{
    /// <summary>
    /// The vector IDs to fetch. Does not accept values containing spaces.
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// Max number of ids to return
    /// </summary>
    public uint? Limit { get; set; }

    /// <summary>
    /// Pagination token to continue a previous listing operation
    /// </summary>
    public string? PaginationToken { get; set; }

    public string? Namespace { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

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
}
