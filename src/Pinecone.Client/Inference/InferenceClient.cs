using System.Net.Http;
using System.Text.Json;
using Pinecone.Client;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client;

public class InferenceClient
{
    private RawClient _client;

    public InferenceClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Generate embeddings for input data
    /// </summary>
    public async Task<EmbeddingsList> EmbedAsync(EmbedRequest request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "embed",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<EmbeddingsList>(responseBody)!;
        }
        throw new Exception(responseBody);
    }
}
