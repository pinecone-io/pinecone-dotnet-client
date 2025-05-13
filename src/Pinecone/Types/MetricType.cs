using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<MetricType>))]
public enum MetricType
{
    [EnumMember(Value = "cosine")]
    Cosine,

    [EnumMember(Value = "euclidean")]
    Euclidean,

    [EnumMember(Value = "dotproduct")]
    Dotproduct,
}
