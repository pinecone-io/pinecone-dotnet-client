using System.Text.Json.Serialization;
using Pinecone.Core;
using Proto = Pinecone.Grpc;

namespace Pinecone;

public record DescribeIndexStatsRequest
{
    /// <summary>
    /// If this parameter is present, the operation only returns statistics
    ///  for vectors that satisfy the filter.
    ///  See https://docs.pinecone.io/guides/data/filtering-with-metadata.
    /// </summary>
    [JsonPropertyName("filter")]
    public Metadata? Filter { get; set; }

    /// <summary>
    /// Maps the DescribeIndexStatsRequest type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.DescribeIndexStatsRequest ToProto()
    {
        var result = new Proto.DescribeIndexStatsRequest();
        if (Filter != null)
        {
            result.Filter = Filter.ToProto();
        }
        return result;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
