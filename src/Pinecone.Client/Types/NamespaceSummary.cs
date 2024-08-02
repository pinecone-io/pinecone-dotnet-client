using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone.Client;

public record NamespaceSummary
{
    /// <summary>
    /// The number of vectors stored in this namespace. Note that updates to this field may lag behind updates to the
    /// underlying index and corresponding query results, etc.
    /// </summary>
    [JsonPropertyName("vectorCount")]
    public uint? VectorCount { get; set; }
    
    #region Mappers
    
    public static NamespaceSummary FromProto(Proto.NamespaceSummary proto)
    {
        return new NamespaceSummary
        {
            VectorCount = proto.VectorCount,
        };
    }
    
    #endregion
}
