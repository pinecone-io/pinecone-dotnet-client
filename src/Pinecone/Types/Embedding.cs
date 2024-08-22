using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record Embedding
{
    /// <summary>
    /// The embedding values.
    /// </summary>
    [JsonPropertyName("values")]
    public IEnumerable<double>? Values { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
