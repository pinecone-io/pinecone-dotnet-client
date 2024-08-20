using System.Text.Json.Serialization;

#nullable enable

namespace Pinecone;

public record CreateCollectionRequest
{
    /// <summary>
    /// The name of the collection to be created. Resource name must be 1-45 characters long, start and end with an alphanumeric character, and consist only of lower case alphanumeric characters or '-'.
    ///
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// The name of the index to be used as the source for the collection.
    /// </summary>
    [JsonPropertyName("source")]
    public required string Source { get; set; }
}
