using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record UpdateRequest
{
    /// <summary>
    /// Vector's unique id.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Vector data.
    /// </summary>
    [JsonPropertyName("values")]
    public IEnumerable<float>? Values { get; set; }

    [JsonPropertyName("sparseValues")]
    public SparseValues? SparseValues { get; set; }

    /// <summary>
    /// Metadata to set for the vector.
    /// </summary>
    [JsonPropertyName("setMetadata")]
    public Metadata? SetMetadata { get; set; }

    /// <summary>
    /// The namespace containing the vector to update.
    /// </summary>
    [JsonPropertyName("namespace")]
    public string? Namespace { get; set; }

    #region Mappers

    public Proto.UpdateRequest ToProto()
    {
        var updateRequest = new Proto.UpdateRequest { Id = Id, };
        if (Values != null && Values.Any())
        {
            updateRequest.Values.AddRange(Values);
        }
        if (SparseValues != null)
        {
            updateRequest.SparseValues = SparseValues.ToProto();
        }
        if (SetMetadata != null)
        {
            updateRequest.SetMetadata = SetMetadata.ToProto();
        }
        if (Namespace != null)
        {
            updateRequest.Namespace = Namespace ?? "";
        }
        return updateRequest;
    }

    #endregion
}
