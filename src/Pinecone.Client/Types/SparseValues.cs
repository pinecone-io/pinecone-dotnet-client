using System.Text.Json.Serialization;
using Google.Protobuf.Collections;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone.Client;

public record SparseValues
{
    [JsonPropertyName("indices")]
    public IEnumerable<uint> Indices { get; set; } = new List<uint>();

    [JsonPropertyName("values")]
    public IEnumerable<float> Values { get; set; } = new List<float>();
    
    #region Mappers
    
    public Proto.SparseValues ToProto()
    {
        var sparseValues = new Proto.SparseValues();
        if (Indices.Any())
        {
            sparseValues.Indices.AddRange(Indices);
        }
        if (Values.Any())
        {
            sparseValues.Values.AddRange(Values);
        }
        return sparseValues;
    }

    public static SparseValues FromProto(Proto.SparseValues proto)
    {
        return new SparseValues
        {
            Indices = proto.Indices.ToList(),
            Values = proto.Values.ToList(),
        };
    }
    
    #endregion
}
