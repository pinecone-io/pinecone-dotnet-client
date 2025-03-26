using System.Text.Json;
using System.Text.Json.Serialization;
using Pinecone.Core;
using ProtoGrpc = Pinecone.Grpc;

namespace Pinecone;

public record Pagination
{
    [JsonPropertyName("next")]
    public string? Next { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Returns a new Pagination type from its Protobuf-equivalent representation.
    /// </summary>
    internal static Pagination FromProto(ProtoGrpc.Pagination value)
    {
        return new Pagination { Next = value.Next };
    }

    /// <summary>
    /// Maps the Pagination type into its Protobuf-equivalent representation.
    /// </summary>
    internal ProtoGrpc.Pagination ToProto()
    {
        var result = new ProtoGrpc.Pagination();
        if (Next != null)
        {
            result.Next = Next ?? "";
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
