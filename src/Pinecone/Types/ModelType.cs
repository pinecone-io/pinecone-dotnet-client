using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<ModelType>))]
public enum ModelType
{
    [EnumMember(Value = "embed")]
    Embed,

    [EnumMember(Value = "rerank")]
    Rerank,
}
