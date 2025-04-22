using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone.Inference;

[JsonConverter(typeof(EnumSerializer<ModelsListRequestVectorType>))]
public enum ModelsListRequestVectorType
{
    [EnumMember(Value = "dense")]
    Dense,

    [EnumMember(Value = "sparse")]
    Sparse,
}
