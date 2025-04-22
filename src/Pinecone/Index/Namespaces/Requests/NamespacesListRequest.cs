using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

namespace Pinecone.Index;

public record NamespacesListRequest
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
    /// Maps the NamespacesListRequest type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.NamespacesListRequest ToProto()
    {
        var result = new Proto.NamespacesListRequest();
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
