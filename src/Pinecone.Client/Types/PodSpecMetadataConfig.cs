using System.Text.Json.Serialization;

#nullable enable

namespace Pinecone.Client;

public record PodSpecMetadataConfig
{
    /// <summary>
    /// By default, all metadata is indexed; to change this behavior, use this property to specify an array of metadata fields that should be indexed.
    /// </summary>
    [JsonPropertyName("indexed")]
    public IEnumerable<string>? Indexed { get; init; }
}
