using System.Text.Json.Serialization;
using Pinecone.Client.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone.Client;

public record ScoredVector
{
    /// <summary>
    /// This is the vector's unique id.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// This is a measure of similarity between this vector and the query vector. The higher the score, the more they are similar.
    /// </summary>
    [JsonPropertyName("score")]
    public float? Score { get; set; }

    /// <summary>
    /// This is the vector data, if it is requested.
    /// </summary>
    [JsonPropertyName("values")]
    public IEnumerable<float>? Values { get; set; }

    /// <summary>
    /// This is the sparse data, if it is requested.
    /// </summary>
    [JsonPropertyName("sparseValues")]
    public SparseValues? SparseValues { get; set; }

    /// <summary>
    /// This is the metadata, if it is requested.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, MetadataValue?>? Metadata { get; set; }
    
    #region Mappers
    
    public static ScoredVector FromProto(Proto.ScoredVector proto)
    {
        return new ScoredVector
        {
            Id = proto.Id,
            Score = proto.Score,
            Values = proto.Values?.ToList(),
            SparseValues = proto.SparseValues != null ? SparseValues.FromProto(proto.SparseValues) : null,
            Metadata = proto.Metadata != null ? ProtoConverter.FromProtoStruct(proto.Metadata) : null,
        };
    }
    
    #endregion
}
