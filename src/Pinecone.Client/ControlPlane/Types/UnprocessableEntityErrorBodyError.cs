using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record UnprocessableEntityErrorBodyError
{
    [JsonPropertyName("code")]
    public required UnprocessableEntityErrorBodyErrorCode Code { get; init; }

    [JsonPropertyName("message")]
    public required string Message { get; init; }

    /// <summary>
    /// Additional information about the error. This field is not guaranteed to be present.
    /// </summary>
    [JsonPropertyName("details")]
    public Dictionary<string, object>? Details { get; init; }
}
