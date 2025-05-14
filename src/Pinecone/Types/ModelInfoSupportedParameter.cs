using System.Text.Json;
using System.Text.Json.Serialization;
using OneOf;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// Describes a parameter supported by the model, including parameter value constraints.
/// </summary>
public record ModelInfoSupportedParameter : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The name of the parameter.
    /// </summary>
    [JsonPropertyName("parameter")]
    public required string Parameter { get; set; }

    /// <summary>
    /// The parameter type e.g. 'one_of', 'numeric_range', or 'any'.
    ///
    /// If the type is 'one_of', then 'allowed_values' will be set, and the value specified must be one of the allowed values. 'one_of' is only compatible with value_type 'string' or 'integer'.
    ///
    /// If 'numeric_range', then 'min' and 'max' will be set, then the value specified must adhere to the value_type and must fall within the `[min, max]` range (inclusive).
    ///
    /// If 'any' then any value is allowed, as long as it adheres to the value_type.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    /// <summary>
    /// The type of value the parameter accepts, e.g. 'string', 'integer', 'float', or 'boolean'.
    /// </summary>
    [JsonPropertyName("value_type")]
    public required string ValueType { get; set; }

    /// <summary>
    /// Whether the parameter is required (true) or optional (false).
    /// </summary>
    [JsonPropertyName("required")]
    public required bool Required { get; set; }

    /// <summary>
    /// The allowed parameter values when the type is 'one_of'.
    /// </summary>
    [JsonPropertyName("allowed_values")]
    public IEnumerable<OneOf<string, int>>? AllowedValues { get; set; }

    /// <summary>
    /// The minimum allowed value (inclusive) when the type is 'numeric_range'.
    /// </summary>
    [JsonPropertyName("min")]
    public double? Min { get; set; }

    /// <summary>
    /// The maximum allowed value (inclusive) when the type is 'numeric_range'.
    /// </summary>
    [JsonPropertyName("max")]
    public double? Max { get; set; }

    /// <summary>
    /// The default value for the parameter when a parameter is optional.
    /// </summary>
    [JsonPropertyName("default")]
    public OneOf<string, int, float, bool>? Default { get; set; }

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
