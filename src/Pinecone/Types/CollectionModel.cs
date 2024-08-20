using System.Text.Json.Serialization;
using Pinecone;

#nullable enable

namespace Pinecone;

public record CollectionModel
{
    /// <summary>
    /// The name of the collection.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// The size of the collection in bytes.
    /// </summary>
    [JsonPropertyName("size")]
    public long? Size { get; set; }

    /// <summary>
    /// The status of the collection.
    /// </summary>
    [JsonPropertyName("status")]
    public required CollectionModelStatus Status { get; set; }

    /// <summary>
    /// The dimension of the vectors stored in each record held in the collection.
    /// </summary>
    [JsonPropertyName("dimension")]
    public int? Dimension { get; set; }

    /// <summary>
    /// The number of records stored in the collection.
    /// </summary>
    [JsonPropertyName("vector_count")]
    public int? VectorCount { get; set; }

    /// <summary>
    /// The environment where the collection is hosted.
    /// </summary>
    [JsonPropertyName("environment")]
    public required string Environment { get; set; }
}
