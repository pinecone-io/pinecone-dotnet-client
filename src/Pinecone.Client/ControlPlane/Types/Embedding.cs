using System.Text.Json.Serialization;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record Embedding
{
    /// <summary>
    /// The embedding values.
    /// </summary>
    [JsonPropertyName("values")]
    public IEnumerable<double>? Values { get; init; }
}
