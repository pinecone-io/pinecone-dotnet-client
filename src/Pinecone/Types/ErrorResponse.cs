using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// The response shape used for all error responses.
/// </summary>
public record ErrorResponse : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
