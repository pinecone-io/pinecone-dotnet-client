using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

namespace Pinecone;

[Serializable]
public record FetchRequest
{
    /// <summary>
    /// The vector IDs to fetch. Does not accept values containing spaces.
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    [JsonIgnore]
    public string? Namespace { get; set; }

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

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
