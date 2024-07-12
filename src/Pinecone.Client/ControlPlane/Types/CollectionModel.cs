using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record CollectionModel
{
    /// <summary>
    /// The name of the collection.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// The size of the collection in bytes.
    /// </summary>
    [JsonPropertyName("size")]
    public int? Size { get; init; }

    /// <summary>
    /// The status of the collection.
    /// </summary>
    [JsonPropertyName("status")]
    public required CollectionModelStatus Status { get; init; }

    /// <summary>
    /// The dimension of the vectors stored in each record held in the collection.
    /// </summary>
    [JsonPropertyName("dimension")]
    public int? Dimension { get; init; }

    /// <summary>
    /// The number of records stored in the collection.
    /// </summary>
    [JsonPropertyName("vector_count")]
    public int? VectorCount { get; init; }

    /// <summary>
    /// The environment where the collection is hosted.
    /// </summary>
    [JsonPropertyName("environment")]
    public required string Environment { get; init; }
}
