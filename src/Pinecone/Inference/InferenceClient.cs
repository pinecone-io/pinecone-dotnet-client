using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Pinecone.Core;
using Pinecone.Inference;

namespace Pinecone;

public partial class InferenceClient
{
    private RawClient _client;

    internal InferenceClient(RawClient client)
    {
        _client = client;
        Models = new ModelsClient(_client);
    }

    public ModelsClient Models { get; }

    /// <summary>
    /// Generate vector embeddings for input data. This endpoint uses [Pinecone Inference](https://docs.pinecone.io/guides/index-data/indexing-overview#vector-embedding).
    /// </summary>
    /// <example><code>
    /// await client.Inference.EmbedAsync(
    ///     new EmbedRequest
    ///     {
    ///         Model = "multilingual-e5-large",
    ///         Inputs = new List&lt;EmbedRequestInputsItem&gt;() { new EmbedRequestInputsItem() },
    ///     }
    /// );
    /// </code></example>
    public async Task<EmbeddingsList> EmbedAsync(
        EmbedRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "embed",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<EmbeddingsList>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                switch (response.StatusCode)
                {
                    case 400:
                        throw new BadRequestError(
                            JsonUtils.Deserialize<ErrorResponse>(responseBody)
                        );
                    case 401:
                        throw new UnauthorizedError(
                            JsonUtils.Deserialize<ErrorResponse>(responseBody)
                        );
                    case 500:
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ErrorResponse>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new PineconeApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }

    /// <summary>
    /// Rerank documents according to their relevance to a query.
    ///
    /// For guidance and examples, see [Rerank results](https://docs.pinecone.io/guides/search/rerank-results).
    /// </summary>
    /// <example><code>
    /// await client.Inference.RerankAsync(
    ///     new RerankRequest
    ///     {
    ///         Model = "bge-reranker-v2-m3",
    ///         Query = "What is the capital of France?",
    ///         Documents = new List&lt;Dictionary&lt;string, object?&gt;&gt;()
    ///         {
    ///             new Dictionary&lt;string, object&gt;()
    ///             {
    ///                 { "id", "1" },
    ///                 { "text", "Paris is the capital of France." },
    ///                 { "title", "France" },
    ///                 { "url", "https://example.com" },
    ///             },
    ///         },
    ///     }
    /// );
    /// </code></example>
    public async Task<RerankResult> RerankAsync(
        RerankRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = "rerank",
                    Body = request,
                    ContentType = "application/json",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                return JsonUtils.Deserialize<RerankResult>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        {
            var responseBody = await response.Raw.Content.ReadAsStringAsync();
            try
            {
                switch (response.StatusCode)
                {
                    case 400:
                        throw new BadRequestError(
                            JsonUtils.Deserialize<ErrorResponse>(responseBody)
                        );
                    case 401:
                        throw new UnauthorizedError(
                            JsonUtils.Deserialize<ErrorResponse>(responseBody)
                        );
                    case 500:
                        throw new InternalServerError(
                            JsonUtils.Deserialize<ErrorResponse>(responseBody)
                        );
                }
            }
            catch (JsonException)
            {
                // unable to map error response, throwing generic error
            }
            throw new PineconeApiException(
                $"Error with status code {response.StatusCode}",
                response.StatusCode,
                responseBody
            );
        }
    }
}
