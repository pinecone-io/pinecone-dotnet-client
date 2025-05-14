using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

public record EmbedRequest
{
    /// <summary>
    /// The [model](https://docs.pinecone.io/guides/index-data/create-an-index#embedding-models) to use for embedding generation.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// Additional model-specific parameters. Refer to the [model guide](https://docs.pinecone.io/guides/index-data/create-an-index#embedding-models) for available model parameters.
    /// </summary>
    [JsonPropertyName("parameters")]
    public Dictionary<string, object?>? Parameters { get; set; }

    /// <summary>
    /// List of inputs to generate embeddings for.
    /// </summary>
    [JsonPropertyName("inputs")]
    public IEnumerable<EmbedRequestInputsItem> Inputs { get; set; } =
        new List<EmbedRequestInputsItem>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
