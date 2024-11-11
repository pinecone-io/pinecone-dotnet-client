using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record EmbedRequestParameters
{
    /// <summary>
    /// Common property used to distinguish between types of data.
    /// </summary>
    [JsonPropertyName("input_type")]
    public string? InputType { get; set; }

    /// <summary>
    /// How to handle inputs longer than those supported by the model. If `"END"`, truncate the input sequence at the token limit. If `"NONE"`, return an error when the input exceeds the token limit.
    /// </summary>
    [JsonPropertyName("truncate")]
    public string? Truncate { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
