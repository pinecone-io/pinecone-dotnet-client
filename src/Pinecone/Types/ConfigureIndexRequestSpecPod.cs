using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

public record ConfigureIndexRequestSpecPod
{
    /// <summary>
    /// The number of replicas. Replicas duplicate your index. They provide higher availability and throughput. Replicas can be scaled up or down as your needs change.
    /// </summary>
    [JsonPropertyName("replicas")]
    public int? Replicas { get; set; }

    /// <summary>
    /// The type of pod to use. One of `s1`, `p1`, or `p2` appended with `.` and one of `x1`, `x2`, `x4`, or `x8`.
    /// </summary>
    [JsonPropertyName("pod_type")]
    public string? PodType { get; set; }

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
