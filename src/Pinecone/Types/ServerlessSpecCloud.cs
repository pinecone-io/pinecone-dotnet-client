using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

[JsonConverter(typeof(StringEnumSerializer<ServerlessSpecCloud>))]
public enum ServerlessSpecCloud
{
    [EnumMember(Value = "gcp")]
    Gcp,

    [EnumMember(Value = "aws")]
    Aws,

    [EnumMember(Value = "azure")]
    Azure
}
