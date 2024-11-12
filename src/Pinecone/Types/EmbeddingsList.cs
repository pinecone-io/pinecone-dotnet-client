using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record EmbeddingsList
{
    /// <summary>
    /// The model used to generate the embeddings
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// The embeddings generated for the inputs.
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<Embedding> Data { get; set; } = new List<Embedding>();

    /// <summary>
    /// Usage statistics for the model inference.
    /// </summary>
    [JsonPropertyName("usage")]
    public required EmbeddingsListUsage Usage { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
