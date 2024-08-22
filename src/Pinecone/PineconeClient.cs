using Pinecone.Core;

namespace Pinecone;

public class PineconeClient : BasePinecone
{
    private readonly string? _apiKey;

    public PineconeClient(string? apiKey = "", ClientOptions? clientOptions = null)
        : base(apiKey, PrepareClientOptions(apiKey, clientOptions))
    {
        _apiKey = apiKey;
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

        clientOptions = PrepareClientOptions(_apiKey, clientOptions);
        var client = new RawClient(
            new ClientOptions
            {
                BaseUrl = NormalizeHost(host),
                HttpClient = clientOptions.HttpClient,
                MaxRetries = clientOptions.MaxRetries,
                Timeout = clientOptions.Timeout,
                GrpcOptions = clientOptions.GrpcOptions,
                Headers = clientOptions.Headers,
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
            ["X-Pinecone-API-Version"] = "2024-07",
            ["X-Fern-Language"] = "C#",
            ["X-Fern-SDK-Name"] = "Pinecone",
            ["X-Fern-SDK-Version"] = "1.0.0-rc0",
            ["User-Agent"] = "lang=C#; version=1.0.0-rc0"
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
                $"lang=C#; version=1.0.0-rc0; source_tag={clientOptions.SourceTag}";
        }
        return clientOptions;
    }

    private string NormalizeHost(string host)
    {
        if (host.StartsWith("https://") || host.StartsWith("http://"))
        {
            return host;
        }
        return "https://" + host;
    }

    private static string GetFromEnvironmentOrThrow(string env, string message)
    {
        return Environment.GetEnvironmentVariable(env) ?? throw new Exception(message);
    }
}
