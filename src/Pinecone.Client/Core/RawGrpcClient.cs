using Grpc.Core;
using Pinecone.Grpc;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;

namespace Pinecone.Client.Core;

#nullable enable
/// <summary>
/// Utility class for making gRPC requests to the API.
/// </summary>
public class RawGrpcClient
{
    public VectorService.VectorServiceClient VectorServiceClient;
    
    /// <summary>
    /// The client options applied on every request.
    /// </summary>
    private readonly ClientOptions _clientOptions;

    /// <summary>
    /// Global headers to be sent with every request. Note that we make
    /// a copy for immutability.
    /// </summary>
    private readonly Dictionary<string, string> _headers;
    
    public RawGrpcClient(Dictionary<string, string> headers, ClientOptions clientOptions)
    {
        _clientOptions = clientOptions;
        _headers = new Dictionary<string, string>(headers);

        var grpcOptions = PrepareGrpcChannelOptions();
        var channel = grpcOptions != null
            ? GrpcChannel.ForAddress(_clientOptions.BaseUrl, grpcOptions)
            : GrpcChannel.ForAddress(_clientOptions.BaseUrl);
        VectorServiceClient = new VectorService.VectorServiceClient(channel); 
    }

    /// <summary>
    /// Prepares the gRPC metadata associated with the given request.
    /// 
    /// The provided request headers take precedence over the headers
    /// associated with this client (which are sent on _every_ request).
    /// </summary>
    public CallOptions CreateCallOptions(GrpcRequestOptions options)
    {
        // Prepare the gRPC metadata (i.e. headers).
        var metadata = new Metadata();
        foreach (var header in _headers)
        {
            metadata.Add(header.Key, header.Value);
        }
        foreach (var header in options.Headers)
        {
            metadata.Add(header.Key, header.Value);
        }
        
        // Configure the gRPC deadline.
        var timeout = options.Timeout ?? _clientOptions.Timeout;
        var deadline = DateTime.UtcNow.Add(timeout);

        return new CallOptions(
            metadata,
            deadline,
            options.CancellationToken,
            options.WriteOptions,
            propagationToken: null,
            options.CallCredentials
        );
    }

    private GrpcChannelOptions? PrepareGrpcChannelOptions()
    {
        var grpcChannelOptions = _clientOptions.GrpcOptions;
        if (grpcChannelOptions == null)
        {
            return null;
        }

        grpcChannelOptions.HttpClient ??= _clientOptions.HttpClient;
        grpcChannelOptions.MaxRetryAttempts ??= _clientOptions.MaxRetries;
        
        return grpcChannelOptions;
    }
}