using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<CreateIndexForModelRequestCloud>))]
public enum CreateIndexForModelRequestCloud
{
    [EnumMember(Value = "gcp")]
    Gcp,

    [EnumMember(Value = "aws")]
    Aws,

    [EnumMember(Value = "azure")]
    Azure,
}
