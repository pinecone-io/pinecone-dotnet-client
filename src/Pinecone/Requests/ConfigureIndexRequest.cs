using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

public record ConfigureIndexRequest
{
    [JsonPropertyName("spec")]
    public ConfigureIndexRequestSpec? Spec { get; set; }

    [JsonPropertyName("deletion_protection")]
    public DeletionProtection? DeletionProtection { get; set; }

    [JsonPropertyName("tags")]
    public Dictionary<string, string>? Tags { get; set; }

    /// <summary>
    /// Configure the integrated inference embedding settings for this index.
    ///
    /// You can convert an existing index to an integrated index by specifying the embedding model and field_map. The index vector type and dimension must match the model vector type and dimension, and the index similarity metric must be supported by the model. Refer to the [model guide](https://docs.pinecone.io/guides/inference/understanding-inference#embedding-models) for available models and model details.
    ///
    /// You can later change the embedding configuration to update the field map, read parameters, or write parameters. Once set, the model cannot be changed.
    /// </summary>
    [JsonPropertyName("embed")]
    public ConfigureIndexRequestEmbed? Embed { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
