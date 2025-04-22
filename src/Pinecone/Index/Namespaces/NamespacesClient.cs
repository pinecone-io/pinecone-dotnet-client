using System.Threading;
using Grpc.Core;
using Pinecone;
using Pinecone.Core;
using Pinecone.Grpc;

namespace Pinecone.Index;

public partial class NamespacesClient
{
    private RawClient _client;

    private RawGrpcClient _grpc;

    private VectorService.VectorServiceClient _vectorService;

    internal NamespacesClient(RawClient client)
    {
        _client = client;
        _grpc = _client.Grpc;
        _vectorService = new VectorService.VectorServiceClient(_grpc.Channel);
    }

    /// <summary>
    /// Get list of all namespaces
    ///
    ///  Get a list of all namespaces within an index.
    /// </summary>
    /// <example><code>
    /// await client.Index.Namespaces.ListAsync(new NamespacesListRequest());
    /// </code></example>
    public async Task<ListNamespacesResponse> ListAsync(
        NamespacesListRequest request,
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
            return ListNamespacesResponse.FromProto(response);
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
    /// Describe a namespace
    ///
    ///  Describe a namespace within an index, showing the vector count within the namespace.
    /// </summary>
    /// <example><code>
    /// await client.Index.Namespaces.GetAsync("namespace");
    /// </code></example>
    public async Task<NamespaceDescription> GetAsync(
        string namespace_,
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
            var call = _vectorService.GetAsync(null, callOptions);
            var response = await call.ConfigureAwait(false);
            return NamespaceDescription.FromProto(response);
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
    /// Delete a namespace
    ///
    ///  Delete a namespace from an index.
    /// </summary>
    /// <example><code>
    /// await client.Index.Namespaces.DeleteAsync("namespace");
    /// </code></example>
    public async Task<DeleteResponse> DeleteAsync(
        string namespace_,
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
            var call = _vectorService.DeleteAsync(null, callOptions);
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
}
