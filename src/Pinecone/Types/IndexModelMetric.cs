using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<IndexModelMetric>))]
public enum IndexModelMetric
{
    [EnumMember(Value = "cosine")]
    Cosine,

    [EnumMember(Value = "euclidean")]
    Euclidean,

    [EnumMember(Value = "dotproduct")]
    Dotproduct,
}
