using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Grpc.Core;
using Pinecone.Core;
using Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public partial class IndexClient
{
    private RawClient _client;

    private RawGrpcClient _grpc;

    private VectorService.VectorServiceClient _vectorService;

    internal IndexClient(RawClient client)
    {
        _client = client;
        _grpc = _client.Grpc;
        _vectorService = new VectorService.VectorServiceClient(_grpc.Channel);
    }

    /// <summary>
    /// The `list_imports` operation lists all recent and ongoing import operations. For guidance and examples, see [Import data](https://docs.pinecone.io/guides/data/import-data).
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Index.ListBulkImportsAsync(new ListBulkImportsRequest());
    /// </code>
    /// </example>
    public async Task<ListImportsResponse> ListBulkImportsAsync(
        ListBulkImportsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.ToString();
        }
        if (request.PaginationToken != null)
        {
            _query["paginationToken"] = request.PaginationToken;
        }
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "bulk/imports",
                Query = _query,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ListImportsResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        throw new PineconeApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <summary>
    /// The `start_import` operation starts an asynchronous import of vectors from object storage into an index. For guidance and examples, see [Import data](https://docs.pinecone.io/guides/data/import-data).
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Index.StartBulkImportAsync(new StartImportRequest { Uri = "uri" });
    /// </code>
    /// </example>
    public async Task<StartImportResponse> StartBulkImportAsync(
        StartImportRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "bulk/imports",
                Body = request,
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<StartImportResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        throw new PineconeApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <summary>
    /// The `describe_import` operation returns details of a specific import operation. For guidance and examples,
    /// see [Import data](https://docs.pinecone.io/guides/data/import-data).
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Index.DescribeBulkImportAsync("101");
    /// </code>
    /// </example>
    public async Task<ImportModel> DescribeBulkImportAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"bulk/imports/{id}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<ImportModel>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        throw new PineconeApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
    }

    /// <summary>
    /// The `cancel_import` operation cancels an import operation if it is not yet finished. It has no effect if the operation is already finished. For guidance and examples, see [Import data](https://docs.pinecone.io/guides/data/import-data).
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Index.CancelBulkImportAsync("101");
    /// </code>
    /// </example>
    public async Task<CancelImportResponse> CancelBulkImportAsync(
        string id,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"bulk/imports/{id}",
                Options = options,
            },
            cancellationToken
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CancelImportResponse>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        throw new PineconeApiException(
            $"Error with status code {response.StatusCode}",
            response.StatusCode,
            responseBody
        );
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
    /// <example>
    /// <code>
    /// await client.Index.DescribeIndexStatsAsync(new DescribeIndexStatsRequest());
    /// </code>
    /// </example>
    public async Task<DescribeIndexStatsResponse> DescribeIndexStatsAsync(
        DescribeIndexStatsRequest request,
        GrpcRequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var callOptions = _grpc.CreateCallOptions(
                options ?? new GrpcRequestOptions(),
                cancellationToken
            );
            var call = _vectorService.DescribeIndexStatsAsync(request.ToProto(), callOptions);
            var response = await call.ConfigureAwait(false);
            return DescribeIndexStatsResponse.FromProto(response);
        }
        catch (RpcException rpc)
        {
            var statusCode = (int)rpc.StatusCode;
            throw new PineconeApiException(
                $"Error with gRPC status code {statusCode}",
                statusCode,
                rpc.Message
            );
        }
        catch (Exception e)
        {
            throw new PineconeException("Error", e);
        }
    }

    /// <summary>
    /// Query vectors
    ///
    /// The `query` operation searches a namespace, using a query vector. It retrieves the ids of the most similar items in a namespace, along with their similarity scores.
    ///
    /// For guidance and examples, see [Query data](https://docs.pinecone.io/guides/data/query-data).
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Index.QueryAsync(
    ///     new QueryRequest
    ///     {
    ///         TopK = 3,
    ///         Namespace = "example",
    ///         IncludeValues = true,
    ///         IncludeMetadata = true,
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<QueryResponse> QueryAsync(
        QueryRequest request,
        GrpcRequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var callOptions = _grpc.CreateCallOptions(
                options ?? new GrpcRequestOptions(),
                cancellationToken
            );
            var call = _vectorService.QueryAsync(request.ToProto(), callOptions);
            var response = await call.ConfigureAwait(false);
            return QueryResponse.FromProto(response);
        }
        catch (RpcException rpc)
        {
            var statusCode = (int)rpc.StatusCode;
            throw new PineconeApiException(
                $"Error with gRPC status code {statusCode}",
                statusCode,
                rpc.Message
            );
        }
        catch (Exception e)
        {
            throw new PineconeException("Error", e);
        }
    }

    /// <summary>
    /// Delete vectors
    ///
    /// The `delete` operation deletes vectors, by id, from a single namespace.
    ///
    /// For guidance and examples, see [Delete data](https://docs.pinecone.io/guides/data/delete-data).
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Index.DeleteAsync(
    ///     new DeleteRequest
    ///     {
    ///         Ids = new List&lt;string&gt;() { "v1", "v2", "v3" },
    ///         Namespace = "example",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<DeleteResponse> DeleteAsync(
        DeleteRequest request,
        GrpcRequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var callOptions = _grpc.CreateCallOptions(
                options ?? new GrpcRequestOptions(),
                cancellationToken
            );
            var call = _vectorService.DeleteAsync(request.ToProto(), callOptions);
            var response = await call.ConfigureAwait(false);
            return DeleteResponse.FromProto(response);
        }
        catch (RpcException rpc)
        {
            var statusCode = (int)rpc.StatusCode;
            throw new PineconeApiException(
                $"Error with gRPC status code {statusCode}",
                statusCode,
                rpc.Message
            );
        }
        catch (Exception e)
        {
            throw new PineconeException("Error", e);
        }
    }

    /// <summary>
    /// Fetch vectors
    ///
    /// The `fetch` operation looks up and returns vectors, by ID, from a single namespace. The returned vectors include the vector data and/or metadata.
    ///
    /// For guidance and examples, see [Fetch data](https://docs.pinecone.io/guides/data/fetch-data).
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Index.FetchAsync(new FetchRequest { Ids = ["v1"], Namespace = "example" });
    /// </code>
    /// </example>
    public async Task<FetchResponse> FetchAsync(
        FetchRequest request,
        GrpcRequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var callOptions = _grpc.CreateCallOptions(
                options ?? new GrpcRequestOptions(),
                cancellationToken
            );
            var call = _vectorService.FetchAsync(request.ToProto(), callOptions);
            var response = await call.ConfigureAwait(false);
            return FetchResponse.FromProto(response);
        }
        catch (RpcException rpc)
        {
            var statusCode = (int)rpc.StatusCode;
            throw new PineconeApiException(
                $"Error with gRPC status code {statusCode}",
                statusCode,
                rpc.Message
            );
        }
        catch (Exception e)
        {
            throw new PineconeException("Error", e);
        }
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
    /// <example>
    /// <code>
    /// await client.Index.ListAsync(
    ///     new ListRequest
    ///     {
    ///         Limit = 50,
    ///         Namespace = "example",
    ///         PaginationToken = "eyJza2lwX3Bhc3QiOiIxMDEwMy0=",
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<ListResponse> ListAsync(
        ListRequest request,
        GrpcRequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var callOptions = _grpc.CreateCallOptions(
                options ?? new GrpcRequestOptions(),
                cancellationToken
            );
            var call = _vectorService.ListAsync(request.ToProto(), callOptions);
            var response = await call.ConfigureAwait(false);
            return ListResponse.FromProto(response);
        }
        catch (RpcException rpc)
        {
            var statusCode = (int)rpc.StatusCode;
            throw new PineconeApiException(
                $"Error with gRPC status code {statusCode}",
                statusCode,
                rpc.Message
            );
        }
        catch (Exception e)
        {
            throw new PineconeException("Error", e);
        }
    }

    /// <summary>
    /// Update a vector
    ///
    /// The `update` operation updates a vector in a namespace. If a value is included, it will overwrite the previous value. If a `set_metadata` is included, the values of the fields specified in it will be added or overwrite the previous value.
    ///
    /// For guidance and examples, see [Update data](https://docs.pinecone.io/guides/data/update-data).
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Index.UpdateAsync(
    ///     new UpdateRequest
    ///     {
    ///         Id = "v1",
    ///         Namespace = "example",
    ///         Values = new[] { 42.2f, 50.5f, 60.8f },
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<UpdateResponse> UpdateAsync(
        UpdateRequest request,
        GrpcRequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var callOptions = _grpc.CreateCallOptions(
                options ?? new GrpcRequestOptions(),
                cancellationToken
            );
            var call = _vectorService.UpdateAsync(request.ToProto(), callOptions);
            var response = await call.ConfigureAwait(false);
            return UpdateResponse.FromProto(response);
        }
        catch (RpcException rpc)
        {
            var statusCode = (int)rpc.StatusCode;
            throw new PineconeApiException(
                $"Error with gRPC status code {statusCode}",
                statusCode,
                rpc.Message
            );
        }
        catch (Exception e)
        {
            throw new PineconeException("Error", e);
        }
    }

    /// <summary>
    /// Upsert vectors
    ///
    /// The `upsert` operation writes vectors into a namespace. If a new value is upserted for an existing vector ID, it will overwrite the previous value.
    ///
    /// For guidance and examples, see [Upsert data](https://docs.pinecone.io/guides/data/upsert-data).
    /// </summary>
    /// <example>
    /// <code>
    /// await client.Index.UpsertAsync(
    ///     new UpsertRequest
    ///     {
    ///         Vectors = new List&lt;Vector&gt;()
    ///         {
    ///             new Vector { Id = "v1", Values = new[] { 0.1f, 0.2f, 0.3f } },
    ///         },
    ///     }
    /// );
    /// </code>
    /// </example>
    public async Task<UpsertResponse> UpsertAsync(
        UpsertRequest request,
        GrpcRequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            var callOptions = _grpc.CreateCallOptions(
                options ?? new GrpcRequestOptions(),
                cancellationToken
            );
            var call = _vectorService.UpsertAsync(request.ToProto(), callOptions);
            var response = await call.ConfigureAwait(false);
            return UpsertResponse.FromProto(response);
        }
        catch (RpcException rpc)
        {
            var statusCode = (int)rpc.StatusCode;
            throw new PineconeApiException(
                $"Error with gRPC status code {statusCode}",
                statusCode,
                rpc.Message
            );
        }
        catch (Exception e)
        {
            throw new PineconeException("Error", e);
        }
    }
}
