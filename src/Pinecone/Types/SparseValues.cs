using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record SparseValues
{
    [JsonPropertyName("indices")]
    public IEnumerable<uint> Indices { get; set; } = new List<uint>();

    [JsonPropertyName("values")]
    public ReadOnlyMemory<float> Values { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the SparseValues type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.SparseValues ToProto()
    {
        var result = new Proto.SparseValues();
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

    /// <summary>
    /// Returns a new SparseValues type from its Protobuf-equivalent representation.
    /// </summary>
    internal static SparseValues FromProto(Proto.SparseValues value)
    {
        return new SparseValues
        {
            Indices = value.Indices?.ToList() ?? new List<uint>(),
            Values = value.Values?.ToArray() ?? new ReadOnlyMemory<float>(),
        };
    }
}
