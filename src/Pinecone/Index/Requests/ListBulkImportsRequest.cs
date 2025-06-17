using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

[Serializable]
public record ListBulkImportsRequest
{
    /// <summary>
    /// Max number of operations to return per page.
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Pagination token to continue a previous listing operation.
    /// </summary>
    [JsonIgnore]
    public string? PaginationToken { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
