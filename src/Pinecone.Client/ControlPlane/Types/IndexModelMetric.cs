using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client.ControlPlane;

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
