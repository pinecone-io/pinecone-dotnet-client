using System.Net.Http;
using Pinecone.Client.Core;

namespace Pinecone.Client;

public class Pinecone : BasePinecone
{
    private readonly string _apiKey;
    
    public Pinecone(string? apiKey = "", ClientOptions? clientOptions = null)
        : base(apiKey, clientOptions)
    {
        apiKey ??= GetFromEnvironmentOrThrow(
            "PINECONE_API_KEY",
            "Please pass in apiKey or set the environment variable PINECONE_API_KEY."
        );
        this._apiKey = apiKey;
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
        
        host ??= DescribeIndexAsync(indexName: name!).Result.Host;
        clientOptions ??= new ClientOptions();
        var client = new RawClient(
            new Dictionary<string, string>()
            {
                { "Api-Key", _apiKey },
                { "X-Pinecone-API-Version", "2024-07" },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "Pinecone.Client" },
                { "X-Fern-SDK-Version", "0.0.98" },
            },
            new Dictionary<string, Func<string>>() { },
            new ClientOptions
            {
                BaseUrl = NormalizeHost(host),
                HttpClient = clientOptions.HttpClient,
                MaxRetries = clientOptions.MaxRetries,
                Timeout = clientOptions.Timeout
            }
        );
        return new IndexClient(client);
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
