using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client.ControlPlane;

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
