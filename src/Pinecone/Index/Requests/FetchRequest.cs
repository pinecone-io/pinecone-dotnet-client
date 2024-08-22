using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record FetchRequest
{
    /// <summary>
    /// The vector IDs to fetch. Does not accept values containing spaces.
    /// </summary>
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    public string? Namespace { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the FetchRequest type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.FetchRequest ToProto()
    {
        var result = new Proto.FetchRequest();
        if (Ids.Any())
        {
            result.Ids.AddRange(Ids);
        }
        if (Namespace != null)
        {
            result.Namespace = Namespace ?? "";
        }
        return result;
    }
}
