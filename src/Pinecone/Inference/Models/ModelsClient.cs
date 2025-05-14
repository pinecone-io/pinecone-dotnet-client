using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Inference;

public partial class ModelsClient
{
    private RawClient _client;

    internal ModelsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Get available models.
    /// </summary>
    /// <example><code>
    /// await client.Inference.Models.ListAsync(new ListModelsRequest());
    /// </code></example>
    public async Task<ModelInfoList> ListAsync(
        ListModelsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.Type != null)
        {
            _query["type"] = request.Type.Value.Stringify();
        }
        if (request.VectorType != null)
        {
            _query["vector_type"] = request.VectorType.Value.Stringify();
        }
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "models",
                    Query = _query,
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
                return JsonUtils.Deserialize<ModelInfoList>(responseBody)!;
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
                    case 401:
                        throw new UnauthorizedError(
                            JsonUtils.Deserialize<ErrorResponse>(responseBody)
                        );
                    case 404:
                        throw new NotFoundError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
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
    /// Get model details.
    /// </summary>
    /// <example><code>
    /// await client.Inference.Models.GetAsync("multilingual-e5-large");
    /// </code></example>
    public async Task<ModelInfo> GetAsync(
        string modelName,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = string.Format(
                        "models/{0}",
                        ValueConvert.ToPathParameterString(modelName)
                    ),
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
                return JsonUtils.Deserialize<ModelInfo>(responseBody)!;
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
                    case 401:
                        throw new UnauthorizedError(
                            JsonUtils.Deserialize<ErrorResponse>(responseBody)
                        );
                    case 404:
                        throw new NotFoundError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
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
