using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

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
    public ReadOnlyMemory<float>? Values { get; set; }

    /// <summary>
    /// This is the sparse data, if it is requested.
    /// </summary>
    [JsonPropertyName("sparseValues")]
    public SparseValues? SparseValues { get; set; }

    /// <summary>
    /// This is the metadata, if it is requested.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Metadata? Metadata { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the ScoredVector type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.ScoredVector ToProto()
    {
        var result = new Proto.ScoredVector();
        result.Id = Id;
        if (Score != null)
        {
            result.Score = Score ?? 0.0f;
        }
        if (Values != null && !Values.Value.IsEmpty)
        {
            result.Values.AddRange(Values.Value.ToArray());
        }
        if (SparseValues != null)
        {
            result.SparseValues = SparseValues.ToProto();
        }
        if (Metadata != null)
        {
            result.Metadata = Metadata.ToProto();
        }
        return result;
    }

    /// <summary>
    /// Returns a new ScoredVector type from its Protobuf-equivalent representation.
    /// </summary>
    internal static ScoredVector FromProto(Proto.ScoredVector value)
    {
        return new ScoredVector
        {
            Id = value.Id,
            Score = value.Score,
            Values = value.Values?.ToArray(),
            SparseValues =
                value.SparseValues != null ? SparseValues.FromProto(value.SparseValues) : null,
            Metadata = value.Metadata != null ? Metadata.FromProto(value.Metadata) : null,
        };
    }
}
