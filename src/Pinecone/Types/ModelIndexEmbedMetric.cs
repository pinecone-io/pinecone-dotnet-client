using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

[JsonConverter(typeof(EnumSerializer<ModelIndexEmbedMetric>))]
public enum ModelIndexEmbedMetric
{
    [EnumMember(Value = "cosine")]
    Cosine,

    [EnumMember(Value = "euclidean")]
    Euclidean,

    [EnumMember(Value = "dotproduct")]
    Dotproduct,
}
