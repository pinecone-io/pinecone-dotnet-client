using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

public record ListRestoreJobsRequest
{
    /// <summary>
    /// The number of results to return per page.
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// The token to use to retrieve the next page of results.
    /// </summary>
    [JsonIgnore]
    public string? PaginationToken { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
