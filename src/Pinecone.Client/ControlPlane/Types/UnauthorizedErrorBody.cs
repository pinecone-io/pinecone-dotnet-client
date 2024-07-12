using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record UnauthorizedErrorBody
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
    public required UnauthorizedErrorBodyError Error { get; init; }
}
