using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

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
    public object? FieldMap { get; set; }

    /// <summary>
    /// The read parameters for the embedding model.
    /// </summary>
    [JsonPropertyName("read_parameters")]
    public object? ReadParameters { get; set; }

    /// <summary>
    /// The write parameters for the embedding model.
    /// </summary>
    [JsonPropertyName("write_parameters")]
    public object? WriteParameters { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
