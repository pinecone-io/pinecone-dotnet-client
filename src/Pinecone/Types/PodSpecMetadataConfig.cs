using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record PodSpecMetadataConfig
{
    /// <summary>
    /// By default, all metadata is indexed; to change this behavior, use this property to specify an array of metadata fields that should be indexed.
    /// </summary>
    [JsonPropertyName("indexed")]
    public IEnumerable<string>? Indexed { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
