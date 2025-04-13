using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// Configuration needed to deploy a serverless index.
/// </summary>
public record ServerlessSpec : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The public cloud where you would like your index hosted.
    /// </summary>
    [JsonPropertyName("cloud")]
    public required ServerlessSpecCloud Cloud { get; set; }

    /// <summary>
    /// The region where you would like your index to be created.
    /// </summary>
    [JsonPropertyName("region")]
    public required string Region { get; set; }

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
