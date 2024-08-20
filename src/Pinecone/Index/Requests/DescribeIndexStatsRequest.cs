using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

#nullable enable

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

    #region Mappers

    public Proto.DescribeIndexStatsRequest ToProto()
    {
        var describeIndexStatsRequest = new Proto.DescribeIndexStatsRequest();
        if (Filter != null)
        {
            describeIndexStatsRequest.Filter = Filter.ToProto();
        }
        return describeIndexStatsRequest;
    }

    #endregion
}
