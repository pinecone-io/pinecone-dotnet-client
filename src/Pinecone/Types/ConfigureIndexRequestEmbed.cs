using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// Configure the integrated inference embedding settings for this index.
///
/// You can convert an existing index to an integrated index by specifying the embedding model and field_map. The index vector type and dimension must match the model vector type and dimension, and the index similarity metric must be supported by the model. Refer to the [model guide](https://docs.pinecone.io/guides/inference/understanding-inference#embedding-models) for available models and model details.
///
/// You can later change the embedding configuration to update the field map, read parameters, or write parameters. Once set, the model cannot be changed.
/// </summary>
public record ConfigureIndexRequestEmbed
{
    /// <summary>
    /// The name of the embedding model to use with the index. The index dimension and model dimension must match, and the index similarity metric must be supported by the model. The index embedding model cannot be changed once set.
    /// </summary>
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    /// <summary>
    /// Identifies the name of the text field from your document model that will be embedded.
    /// </summary>
    [JsonPropertyName("field_map")]
    public Dictionary<string, object?>? FieldMap { get; set; }

    /// <summary>
    /// The read parameters for the embedding model.
    /// </summary>
    [JsonPropertyName("read_parameters")]
    public Dictionary<string, object?>? ReadParameters { get; set; }

    /// <summary>
    /// The write parameters for the embedding model.
    /// </summary>
    [JsonPropertyName("write_parameters")]
    public Dictionary<string, object?>? WriteParameters { get; set; }

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
