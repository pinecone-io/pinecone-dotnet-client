using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<ModelInfoType>))]
public enum ModelInfoType
{
    [EnumMember(Value = "embed")]
    Embed,

    [EnumMember(Value = "rerank")]
    Rerank,
}
