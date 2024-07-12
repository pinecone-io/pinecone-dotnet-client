using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone.Client;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateIndexRequestMetric>))]
public enum CreateIndexRequestMetric
{
    [EnumMember(Value = "cosine")]
    Cosine,

    [EnumMember(Value = "euclidean")]
    Euclidean,

    [EnumMember(Value = "dotproduct")]
    Dotproduct
}
