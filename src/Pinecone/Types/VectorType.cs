using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<VectorType>))]
public enum VectorType
{
    [EnumMember(Value = "dense")]
    Dense,

    [EnumMember(Value = "sparse")]
    Sparse,
}
