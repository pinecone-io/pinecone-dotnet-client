using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The response shape used for all error responses.
/// </summary>
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

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
