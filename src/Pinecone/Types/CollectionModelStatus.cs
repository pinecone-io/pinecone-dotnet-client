using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<CollectionModelStatus>))]
public enum CollectionModelStatus
{
    [EnumMember(Value = "Initializing")]
    Initializing,

    [EnumMember(Value = "Ready")]
    Ready,

    [EnumMember(Value = "Terminating")]
    Terminating,
}
