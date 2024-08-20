using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record DeleteRequest
{
    /// <summary>
    /// Vectors to delete.
    /// </summary>
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

    /// <summary>
    /// This indicates that all vectors in the index namespace should be deleted.
    /// </summary>
    [JsonPropertyName("deleteAll")]
    public bool? DeleteAll { get; set; }

    /// <summary>
    /// The namespace to delete vectors from, if applicable.
    /// </summary>
    [JsonPropertyName("namespace")]
    public string? Namespace { get; set; }

    /// <summary>
    /// If specified, the metadata filter here will be used to select the vectors to delete. This is mutually exclusive
    ///  with specifying ids to delete in the ids param or using `delete_all=True`.
    ///  For guidance and examples, see [Filter with metadata](https://docs.pinecone.io/guides/data/filter-with-metadata).
    ///  Serverless indexes do not support delete by metadata. Instead, you can use the `list` operation to fetch the vector IDs based on their common ID prefix and then delete the records by ID.
    /// </summary>
    [JsonPropertyName("filter")]
    public Dictionary<string, MetadataValue?>? Filter { get; set; }

    #region Mappers

    public Proto.DeleteRequest ToProto()
    {
        var deleteRequest = new Proto.DeleteRequest();
        if (Ids != null && Ids.Any())
        {
            deleteRequest.Ids.AddRange(Ids);
        }
        if (DeleteAll != null)
        {
            deleteRequest.DeleteAll = DeleteAll ?? false;
        }
        if (Namespace != null)
        {
            deleteRequest.Namespace = Namespace ?? "";
        }
        if (Filter != null)
        {
            deleteRequest.Filter = Core.ProtoConverter.ToProtoStruct(Filter);
        }
        return deleteRequest;
    }

    #endregion
}
