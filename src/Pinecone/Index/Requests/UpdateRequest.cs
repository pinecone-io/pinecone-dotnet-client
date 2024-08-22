using System.Text.Json.Serialization;
using Pinecone.Core;
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
    public ReadOnlyMemory<float>? Values { get; set; }

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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the UpdateRequest type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.UpdateRequest ToProto()
    {
        var result = new Proto.UpdateRequest();
        result.Id = Id;
        if (Values != null && !Values.Value.IsEmpty)
        {
            result.Values.AddRange(Values.Value.ToArray());
        }
        if (SparseValues != null)
        {
            result.SparseValues = SparseValues.ToProto();
        }
        if (SetMetadata != null)
        {
            result.SetMetadata = SetMetadata.ToProto();
        }
        if (Namespace != null)
        {
            result.Namespace = Namespace ?? "";
        }
        return result;
    }
}
