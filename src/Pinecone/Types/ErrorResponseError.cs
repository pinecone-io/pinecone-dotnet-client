using System.Text.Json.Serialization;
using Pinecone;

#nullable enable

namespace Pinecone;

public record ErrorResponseError
{
    [JsonPropertyName("code")]
    public required ErrorResponseErrorCode Code { get; set; }

    [JsonPropertyName("message")]
    public required string Message { get; set; }

    /// <summary>
    /// Additional information about the error. This field is not guaranteed to be present.
    /// </summary>
    [JsonPropertyName("details")]
    public Dictionary<string, object?>? Details { get; set; }
}
