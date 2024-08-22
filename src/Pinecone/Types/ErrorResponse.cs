using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record ErrorResponse
{
    /// <summary>
    /// The HTTP status code of the error.
    /// </summary>
    [JsonPropertyName("status")]
    public required int Status { get; set; }

    /// <summary>
    /// Detailed information about the error that occurred.
    /// </summary>
    [JsonPropertyName("error")]
    public required ErrorResponseError Error { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
