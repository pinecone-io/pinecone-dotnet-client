using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// Configuration needed to deploy a serverless index.
/// </summary>
public record ServerlessSpec
{
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
