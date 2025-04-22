using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<ModelInfoVectorType>))]
public enum ModelInfoVectorType
{
    [EnumMember(Value = "dense")]
    Dense,

    [EnumMember(Value = "sparse")]
    Sparse,
}
