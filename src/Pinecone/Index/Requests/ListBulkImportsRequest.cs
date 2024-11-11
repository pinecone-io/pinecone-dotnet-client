using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record ListBulkImportsRequest
{
    /// <summary>
    /// Max number of operations to return per page.
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Pagination token to continue a previous listing operation.
    /// </summary>
    public string? PaginationToken { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
