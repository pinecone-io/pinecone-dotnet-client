using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<ServerlessSpecCloud>))]
public enum ServerlessSpecCloud
{
    [EnumMember(Value = "gcp")]
    Gcp,

    [EnumMember(Value = "aws")]
    Aws,

    [EnumMember(Value = "azure")]
    Azure,
}
