using System.Net.Http;
using System.Text.Json;
using Pinecone.Client;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client;

public class IndexClient
{
    private RawClient _client;
    private RawGrpcClient _grpcClient;

    public IndexClient(RawClient client)
    {
        _client = client;
        _grpcClient = client.Grpc();
    }

    /// <summary>
    /// Get index stats
    ///
    /// The `describe_index_stats` operation returns statistics about the contents of an index, including the vector count per namespace, the number of dimensions, and the index fullness.
    ///
    /// Serverless indexes scale automatically as needed, so index fullness is relevant only for pod-based indexes.
    ///
    /// For pod-based indexes, the index fullness result may be inaccurate during pod resizing; to get the status of a pod resizing process, use [`describe_index`](https://docs.pinecone.io/reference/api/control-plane/describe_index).
    /// </summary>
    public async Task<DescribeIndexStatsResponse> DescribeIndexStatsAsync(
        DescribeIndexStatsRequest request,
        GrpcRequestOptions? options = null
    )
    {
        var callOptions = _grpcClient.CreateCallOptions(options ?? new GrpcRequestOptions());
        var call = _grpcClient.VectorServiceClient.DescribeIndexStatsAsync(
            request.ToProto(),
            callOptions
        );
        var response = await call.ConfigureAwait(false);
        return DescribeIndexStatsResponse.FromProto(response);
    }

    /// <summary>
    /// Query vectors
    ///
    /// The `query` operation searches a namespace, using a query vector. It retrieves the ids of the most similar items in a namespace, along with their similarity scores.
    ///
    /// For guidance and examples, see [Query data](https://docs.pinecone.io/guides/data/query-data).
    /// </summary>
    public async Task<QueryResponse> QueryAsync(
        QueryRequest request,
        GrpcRequestOptions? options = null
    )
    {
        var callOptions = _grpcClient.CreateCallOptions(options ?? new GrpcRequestOptions());
        var call = _grpcClient.VectorServiceClient.QueryAsync(request.ToProto(), callOptions);
        var response = await call.ConfigureAwait(false);
        return QueryResponse.FromProto(response);
    }

    /// <summary>
    /// Delete vectors
    ///
    /// The `delete` operation deletes vectors, by id, from a single namespace.
    ///
    /// For guidance and examples, see [Delete data](https://docs.pinecone.io/guides/data/delete-data).
    /// </summary>
    public async Task<DeleteResponse> DeleteAsync(
        DeleteRequest request,
        GrpcRequestOptions? options = null
    )
    {
        var callOptions = _grpcClient.CreateCallOptions(options ?? new GrpcRequestOptions());
        var call = _grpcClient.VectorServiceClient.DeleteAsync(request.ToProto(), callOptions);
        var response = await call.ConfigureAwait(false);
        return DeleteResponse.FromProto(response);
    }

    /// <summary>
    /// Fetch vectors
    ///
    /// The `fetch` operation looks up and returns vectors, by ID, from a single namespace. The returned vectors include the vector data and/or metadata.
    ///
    /// For guidance and examples, see [Fetch data](https://docs.pinecone.io/guides/data/fetch-data).
    /// </summary>
    public async Task<FetchResponse> FetchAsync(
        FetchRequest request,
        GrpcRequestOptions? options = null
    )
    {
        var callOptions = _grpcClient.CreateCallOptions(options ?? new GrpcRequestOptions());
        var call = _grpcClient.VectorServiceClient.FetchAsync(request.ToProto(), callOptions);
        var response = await call.ConfigureAwait(false);
        return FetchResponse.FromProto(response);
    }

    /// <summary>
    /// List vector IDs
    ///
    /// The `list` operation lists the IDs of vectors in a single namespace of a serverless index. An optional prefix can be passed to limit the results to IDs with a common prefix.
    ///
    /// `list` returns up to 100 IDs at a time by default in sorted order (bitwise/"C" collation). If the `limit` parameter is set, `list` returns up to that number of IDs instead. Whenever there are additional IDs to return, the response also includes a `pagination_token` that you can use to get the next batch of IDs. When the response does not include a `pagination_token`, there are no more IDs to return.
    ///
    /// For guidance and examples, see [List record IDs](https://docs.pinecone.io/guides/data/list-record-ids).
    ///
    /// **Note:** `list` is supported only for serverless indexes.
    /// </summary>
    public async Task<ListResponse> ListAsync(
        ListRequest request,
        GrpcRequestOptions? options = null
    )
    {
        var callOptions = _grpcClient.CreateCallOptions(options ?? new GrpcRequestOptions());
        var call = _grpcClient.VectorServiceClient.ListAsync(request.ToProto(), callOptions);
        var response = await call.ConfigureAwait(false);
        return ListResponse.FromProto(response);
    }

    /// <summary>
    /// Update a vector
    ///
    /// The `update` operation updates a vector in a namespace. If a value is included, it will overwrite the previous value. If a `set_metadata` is included, the values of the fields specified in it will be added or overwrite the previous value.
    ///
    /// For guidance and examples, see [Update data](https://docs.pinecone.io/guides/data/update-data).
    /// </summary>
    public async Task<UpdateResponse> UpdateAsync(
        UpdateRequest request,
        GrpcRequestOptions? options = null
    )
    {
        var callOptions = _grpcClient.CreateCallOptions(options ?? new GrpcRequestOptions());
        var call = _grpcClient.VectorServiceClient.UpdateAsync(request.ToProto(), callOptions);
        var response = await call.ConfigureAwait(false);
        return UpdateResponse.FromProto(response);
    }

    /// <summary>
    /// Upsert vectors
    ///
    /// The `upsert` operation writes vectors into a namespace. If a new value is upserted for an existing vector ID, it will overwrite the previous value.
    ///
    /// For guidance and examples, see [Upsert data](https://docs.pinecone.io/guides/data/upsert-data).
    /// </summary>
    public async Task<UpsertResponse> UpsertAsync(
        UpsertRequest request,
        GrpcRequestOptions? options = null
    )
    {
        var callOptions = _grpcClient.CreateCallOptions(options ?? new GrpcRequestOptions());
        var call = _grpcClient.VectorServiceClient.UpsertAsync(request.ToProto(), callOptions);
        var response = await call.ConfigureAwait(false);
        return UpsertResponse.FromProto(response);
    }
}
