using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<DeletionProtection>))]
public enum DeletionProtection
{
    [EnumMember(Value = "disabled")]
    Disabled,

    [EnumMember(Value = "enabled")]
    Enabled,
}
