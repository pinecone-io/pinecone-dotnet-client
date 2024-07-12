using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record InternalServerErrorBody
{
    /// <summary>
    /// The HTTP status code of the error.
    /// </summary>
    [JsonPropertyName("status")]
    public required int Status { get; init; }

    /// <summary>
    /// Detailed information about the error that occurred.
    /// </summary>
    [JsonPropertyName("error")]
    public required InternalServerErrorBodyError Error { get; init; }
}
