using System.Data;
using Google.Protobuf.Reflection;
using Proto = Pinecone.Grpc;

namespace Pinecone.Client;

public record FetchRequest
{
    /// <summary>
    /// The vector IDs to fetch. Does not accept values containing spaces.
    /// </summary>
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    public string? Namespace { get; set; }

    #region Mappers

    public Proto.FetchRequest ToProto()
    {
        var fetchRequest = new Proto.FetchRequest();
        if (Ids.Any())
        {
            fetchRequest.Ids.AddRange(Ids);
        }
        if (Namespace != null)
        {
            fetchRequest.Namespace = Namespace ?? "";
        }
        return fetchRequest;
    }

    #endregion
}
