using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Client;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client;

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
