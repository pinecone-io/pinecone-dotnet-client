using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client.ControlPlane;

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
