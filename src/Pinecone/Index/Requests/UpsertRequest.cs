using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

namespace Pinecone;

public record UpsertRequest
{
    /// <summary>
    /// An array containing the vectors to upsert. Recommended batch limit is 100 vectors.
    /// </summary>
    [JsonPropertyName("vectors")]
    public IEnumerable<Vector> Vectors { get; set; } = new List<Vector>();

    /// <summary>
    /// The namespace where you upsert vectors.
    /// </summary>
    [JsonPropertyName("namespace")]
    public string? Namespace { get; set; }

    /// <summary>
    /// Maps the UpsertRequest type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.UpsertRequest ToProto()
    {
        var result = new Proto.UpsertRequest();
        if (Vectors.Any())
        {
            result.Vectors.AddRange(Vectors.Select(elem => elem.ToProto()));
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
