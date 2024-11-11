using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record EmbeddingsListUsage
{
    /// <summary>
    /// Total number of tokens consumed across all inputs.
    /// </summary>
    [JsonPropertyName("total_tokens")]
    public int? TotalTokens { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
