using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

public record SparseValues
{
    [JsonPropertyName("indices")]
    public IEnumerable<uint> Indices { get; set; } = new List<uint>();

    [JsonPropertyName("values")]
    public ReadOnlyMemory<float> Values { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Returns a new SparseValues type from its Protobuf-equivalent representation.
    /// </summary>
    internal static SparseValues FromProto(ProtoGrpc.SparseValues value)
    {
        return new SparseValues
        {
            Indices = value.Indices?.ToList() ?? Enumerable.Empty<uint>(),
            Values = value.Values?.ToArray() ?? new ReadOnlyMemory<float>(),
        };
    }

    /// <summary>
    /// Maps the SparseValues type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.SparseValues ToProto()
    {
        var result = new ProtoGrpc.SparseValues();
        if (Indices.Any())
        {
            result.Indices.AddRange(Indices);
        }
        if (!Values.IsEmpty)
        {
            result.Values.AddRange(Values.ToArray());
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
