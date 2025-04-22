using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone.Inference;

[JsonConverter(typeof(EnumSerializer<ModelsListRequestType>))]
public enum ModelsListRequestType
{
    [EnumMember(Value = "embed")]
    Embed,

    [EnumMember(Value = "rerank")]
    Rerank,
}
