using System.Text.Json.Serialization;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record EmbedRequestParameters
{
    /// <summary>
    /// Common property used to distinguish between types of data.
    /// </summary>
    [JsonPropertyName("input_type")]
    public string? InputType { get; init; }

    /// <summary>
    /// How to handle inputs longer than those supported by the model. If NONE, when the input exceeds the maximum input token length an error will be returned.
    /// </summary>
    [JsonPropertyName("truncate")]
    public string? Truncate { get; init; }
}
