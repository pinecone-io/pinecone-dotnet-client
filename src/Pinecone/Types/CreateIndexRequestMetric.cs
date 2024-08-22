using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

[JsonConverter(typeof(StringEnumSerializer<CreateIndexRequestMetric>))]
public enum CreateIndexRequestMetric
{
    [EnumMember(Value = "cosine")]
    Cosine,

    [EnumMember(Value = "euclidean")]
    Euclidean,

    [EnumMember(Value = "dotproduct")]
    Dotproduct,
}
