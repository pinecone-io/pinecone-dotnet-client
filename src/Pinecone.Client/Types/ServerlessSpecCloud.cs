using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Client;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client;

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
