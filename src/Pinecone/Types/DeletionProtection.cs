using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

[JsonConverter(typeof(StringEnumSerializer<DeletionProtection>))]
public enum DeletionProtection
{
    [EnumMember(Value = "disabled")]
    Disabled,

    [EnumMember(Value = "enabled")]
    Enabled
}
