using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record EmbedRequest
{
    /// <summary>
    /// The [model](https://docs.pinecone.io/guides/inference/understanding-inference#models) to use for embedding generation.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// Model-specific parameters.
    /// </summary>
    [JsonPropertyName("parameters")]
    public EmbedRequestParameters? Parameters { get; set; }

    /// <summary>
    /// List of inputs to generate embeddings for.
    /// </summary>
    [JsonPropertyName("inputs")]
    public IEnumerable<EmbedRequestInputsItem> Inputs { get; set; } =
        new List<EmbedRequestInputsItem>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
