using System.Text.Json.Serialization;
using Google.Protobuf.Reflection;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record UpsertRequest
{
    /// <summary>
    /// An array containing the vectors to upsert. Recommended batch limit is 100 vectors.
    /// </summary>
    [JsonPropertyName("vectors")]
    public IEnumerable<Vector> Vectors { get; set; } = new List<Vector>();

    /// <summary>
    /// The namespace nwhere you upsert vectors.
    /// </summary>
    [JsonPropertyName("namespace")]
    public string? Namespace { get; set; }

    #region Mappers

    public Proto.UpsertRequest ToProto()
    {
        var upsertRequest = new Proto.UpsertRequest();
        if (Vectors.Any())
        {
            // TODO: Test if we can drop the any check.
            upsertRequest.Vectors.AddRange(Vectors.Select(vector => vector.ToProto()));
        }
        if (Namespace != null)
        {
            upsertRequest.Namespace = Namespace ?? "";
        }
        return upsertRequest;
    }

    #endregion
}
