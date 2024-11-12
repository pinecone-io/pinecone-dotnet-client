using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<ImportErrorModeOnError>))]
public enum ImportErrorModeOnError
{
    [EnumMember(Value = "abort")]
    Abort,

    [EnumMember(Value = "continue")]
    Continue,
}
