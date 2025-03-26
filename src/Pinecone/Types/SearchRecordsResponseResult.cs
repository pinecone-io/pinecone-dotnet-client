using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

public record SearchRecordsResponseResult
{
    /// <summary>
    /// The hits for the search document request.
    /// </summary>
    [JsonPropertyName("hits")]
    public IEnumerable<Hit> Hits { get; set; } = new List<Hit>();

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
