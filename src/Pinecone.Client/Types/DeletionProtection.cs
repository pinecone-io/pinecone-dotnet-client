using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Client;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client;

[JsonConverter(typeof(StringEnumSerializer<DeletionProtection>))]
public enum DeletionProtection
{
    [EnumMember(Value = "disabled")]
    Disabled,

    [EnumMember(Value = "enabled")]
    Enabled
}
