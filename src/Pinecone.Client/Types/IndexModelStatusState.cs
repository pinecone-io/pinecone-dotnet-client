using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Client;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client;

[JsonConverter(typeof(StringEnumSerializer<IndexModelStatusState>))]
public enum IndexModelStatusState
{
    [EnumMember(Value = "Initializing")]
    Initializing,

    [EnumMember(Value = "InitializationFailed")]
    InitializationFailed,

    [EnumMember(Value = "ScalingUp")]
    ScalingUp,

    [EnumMember(Value = "ScalingDown")]
    ScalingDown,

    [EnumMember(Value = "ScalingUpPodSize")]
    ScalingUpPodSize,

    [EnumMember(Value = "ScalingDownPodSize")]
    ScalingDownPodSize,

    [EnumMember(Value = "Terminating")]
    Terminating,

    [EnumMember(Value = "Ready")]
    Ready
}
