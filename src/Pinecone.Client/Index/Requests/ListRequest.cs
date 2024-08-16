using Google.Protobuf.Reflection;
using Proto = Pinecone.Grpc;

namespace Pinecone.Client;

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

    #region Mappers

    public Proto.ListRequest ToProto()
    {
        var listRequest = new Proto.ListRequest();
        if (Limit != null)
        {
            listRequest.Limit = Limit ?? 0;
        }
        if (Prefix != null)
        {
            listRequest.Prefix = Prefix ?? "";
        }
        if (PaginationToken != null)
        {
            listRequest.PaginationToken = PaginationToken ?? "";
        }
        if (Namespace != null)
        {
            listRequest.Namespace = Namespace ?? "";
        }
        return listRequest;
    }

    #endregion
}
