using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

[JsonConverter(typeof(StringEnumSerializer<IndexModelMetric>))]
public enum IndexModelMetric
{
    [EnumMember(Value = "cosine")]
    Cosine,

    [EnumMember(Value = "euclidean")]
    Euclidean,

    [EnumMember(Value = "dotproduct")]
    Dotproduct
}
