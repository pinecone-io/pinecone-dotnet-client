using Pinecone.Client.ControlPlane;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client;

public partial class Pinecone
{
    private RawClient _client;

    public Pinecone(string? apiKey = null, ClientOptions? clientOptions = null)
    {
        apiKey ??= GetFromEnvironmentOrThrow(
            "PINECONE_API_KEY",
            "Please pass in apiKey or set the environment variable PINECONE_API_KEY."
        );
        _client = new RawClient(
            new Dictionary<string, string>()
            {
                { "Api-Key", apiKey },
                { "X-Pinecone-API-Version", "2024-07" },
                { "X-Fern-Language", "C#" },
                { "X-Fern-SDK-Name", "Pinecone.Client" },
                { "X-Fern-SDK-Version", "0.0.54" },
            },
            clientOptions ?? new ClientOptions()
        );
        ControlPlane = new ControlPlaneClient(_client);
    }

    public ControlPlaneClient ControlPlane { get; init; }

    private static string GetFromEnvironmentOrThrow(string env, string message)
    {
        return Environment.GetEnvironmentVariable(env) ?? throw new Exception(message);
    }
}
