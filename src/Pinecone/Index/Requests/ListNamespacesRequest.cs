using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

namespace Pinecone;

public record ListNamespacesRequest
{
    /// <summary>
    /// Pagination token to continue a previous listing operation
    /// </summary>
    [JsonIgnore]
    public string? PaginationToken { get; set; }

    /// <summary>
    /// Max number of namespaces to return
    /// </summary>
    [JsonIgnore]
    public uint? Limit { get; set; }

    /// <summary>
    /// Maps the ListNamespacesRequest type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.ListNamespacesRequest ToProto()
    {
        var result = new Proto.ListNamespacesRequest();
        if (PaginationToken != null)
        {
            result.PaginationToken = PaginationToken ?? "";
        }
        if (Limit != null)
        {
            result.Limit = Limit ?? 0;
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
