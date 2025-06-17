using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

/// <summary>
/// The query results for a single `QueryVector`
/// </summary>
[Serializable]
public record SingleQueryResults : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// The matches for the vectors.
    /// </summary>
    [JsonPropertyName("matches")]
    public IEnumerable<ScoredVector>? Matches { get; set; }

    /// <summary>
    /// The namespace for the vectors.
    /// </summary>
    [JsonPropertyName("namespace")]
    public string? Namespace { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    /// <summary>
    /// Returns a new SingleQueryResults type from its Protobuf-equivalent representation.
    /// </summary>
    internal static SingleQueryResults FromProto(ProtoGrpc.SingleQueryResults value)
    {
        return new SingleQueryResults
        {
            Matches = value.Matches?.Select(ScoredVector.FromProto),
            Namespace = value.Namespace,
        };
    }

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <summary>
    /// Maps the SingleQueryResults type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.SingleQueryResults ToProto()
    {
        var result = new ProtoGrpc.SingleQueryResults();
        if (Matches != null && Matches.Any())
        {
            result.Matches.AddRange(Matches.Select(elem => elem.ToProto()));
        }
        if (Namespace != null)
        {
            result.Namespace = Namespace ?? "";
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
