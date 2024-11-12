using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<ImportModelStatus>))]
public enum ImportModelStatus
{
    [EnumMember(Value = "Pending")]
    Pending,

    [EnumMember(Value = "InProgress")]
    InProgress,

    [EnumMember(Value = "Failed")]
    Failed,

    [EnumMember(Value = "Completed")]
    Completed,

    [EnumMember(Value = "Cancelled")]
    Cancelled,
}
