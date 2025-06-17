using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// Embeddings generated for the input.
/// </summary>
[Serializable]
public record EmbeddingsList : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The model used to generate the embeddings
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// Indicates whether the response data contains 'dense' or 'sparse' embeddings.
    /// </summary>
    [JsonPropertyName("vector_type")]
    public required VectorType VectorType { get; set; }

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

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
