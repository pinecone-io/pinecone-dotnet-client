using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

[JsonConverter(typeof(StringEnumSerializer<CollectionModelStatus>))]
public enum CollectionModelStatus
{
    [EnumMember(Value = "Initializing")]
    Initializing,

    [EnumMember(Value = "Ready")]
    Ready,

    [EnumMember(Value = "Terminating")]
    Terminating
}
