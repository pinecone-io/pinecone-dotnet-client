using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record CreateIndexForModelRequest
{
    /// <summary>
    /// The name of the index. Resource name must be 1-45 characters long, start and end with an alphanumeric character, and consist only of lower case alphanumeric characters or '-'.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// The public cloud where you would like your index hosted.
    /// </summary>
    [JsonPropertyName("cloud")]
    public required CreateIndexForModelRequestCloud Cloud { get; set; }

    /// <summary>
    /// The region where you would like your index to be created.
    /// </summary>
    [JsonPropertyName("region")]
    public required string Region { get; set; }

    [JsonPropertyName("deletion_protection")]
    public DeletionProtection? DeletionProtection { get; set; }

    [JsonPropertyName("tags")]
    public Dictionary<string, string>? Tags { get; set; }

    /// <summary>
    /// Specify the integrated inference embedding configuration for the index.
    ///
    /// Once set the model cannot be changed, but you can later update the embedding configuration for an integrated inference index including field map, read parameters, or write parameters.
    ///
    /// Refer to the [model guide](https://docs.pinecone.io/guides/inference/understanding-inference#embedding-models) for available models and model details.
    /// </summary>
    [JsonPropertyName("embed")]
    public required CreateIndexForModelRequestEmbed Embed { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
