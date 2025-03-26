using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// Configuration for the behavior of Pinecone's internal metadata index. By default, all metadata is indexed; when `metadata_config` is present, only specified metadata fields are indexed. These configurations are only valid for use with pod-based indexes.
/// </summary>
public record PodSpecMetadataConfig
{
    /// <summary>
    /// By default, all metadata is indexed; to change this behavior, use this property to specify an array of metadata fields that should be indexed.
    /// </summary>
    [JsonPropertyName("indexed")]
    public IEnumerable<string>? Indexed { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
