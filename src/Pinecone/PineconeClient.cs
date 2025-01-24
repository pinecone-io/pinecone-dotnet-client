using Pinecone.Core;

namespace Pinecone;

public class PineconeClient : BasePinecone
{
    private readonly string? _apiKey;
    private readonly ClientOptions? _rootClientOptions;

    public PineconeClient(string? apiKey = "", ClientOptions? clientOptions = null)
        : base(apiKey, PrepareClientOptions(apiKey, clientOptions))
    {
        _apiKey = apiKey;
        _rootClientOptions = clientOptions;
    }

    public new IndexClient Index(
        string? name = null,
        string? host = null,
        ClientOptions? clientOptions = null
    )
    {
        if (name == null && host == null)
        {
            throw new Exception("Either name or host must be specified");
        }

        try
        {
            host ??= DescribeIndexAsync(indexName: name!).Result.Host;
        }
        catch (AggregateException e)
        {
            throw e.InnerException ?? e;
        }

        clientOptions = PrepareClientOptions(_apiKey, clientOptions ?? _rootClientOptions);
        var client = new RawClient(
            new ClientOptions
            {
                BaseUrl = NormalizeHost(host, clientOptions.IsTlsEnabled),
                HttpClient = clientOptions.HttpClient,
                MaxRetries = clientOptions.MaxRetries,
                Timeout = clientOptions.Timeout,
                GrpcOptions = clientOptions.GrpcOptions,
                Headers = clientOptions.Headers,
                SourceTag = clientOptions.SourceTag,
            }
        );
        return new IndexClient(client);
    }

    private static ClientOptions PrepareClientOptions(
        string? apiKey = "",
        ClientOptions? clientOptions = null
    )
    {
        apiKey ??= GetFromEnvironmentOrThrow(
            "PINECONE_API_KEY",
            "Please pass in apiKey or set the environment variable PINECONE_API_KEY."
        );
        var defaultHeaders = new Headers()
        {
            ["Api-Key"] = apiKey,
            ["X-Pinecone-API-Version"] = "2025-01",
            ["X-Fern-Language"] = "C#",
            ["X-Fern-SDK-Name"] = "Pinecone",
            ["X-Fern-SDK-Version"] = Version.Current,
            ["User-Agent"] = $"lang=C#; version={Version.Current}",
        };
        clientOptions ??= new ClientOptions();
        foreach (var header in defaultHeaders)
        {
            if (!clientOptions.Headers.ContainsKey(header.Key))
            {
                clientOptions.Headers[header.Key] = header.Value;
            }
        }

        if (clientOptions.SourceTag != null)
        {
            clientOptions.Headers["User-Agent"] =
                $"lang=C#; version={Version.Current}; source_tag={clientOptions.SourceTag}";
        }

        return clientOptions;
    }

    private static string NormalizeHost(string host, bool isTlsEnabled)
    {
        if(host.StartsWith("http://") || host.StartsWith("https://"))
        {
            return host;
        }

        return $"{(isTlsEnabled ? "https" : "http")}://{host}";
    }

    private static string GetFromEnvironmentOrThrow(string env, string message)
    {
        return Environment.GetEnvironmentVariable(env) ?? throw new Exception(message);
    }
}