using System;
using System.Net.Http;
using System.Text.Json;
using Pinecone.Client;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client;

public partial class BasePinecone
{
    private RawClient _client;

    public BasePinecone(string? apiKey = null, ClientOptions? clientOptions = null)
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
                { "X-Fern-SDK-Version", "0.0.98" },
            },
            new Dictionary<string, Func<string>>() { },
            clientOptions ?? new ClientOptions()
        );
        Index = new IndexClient(_client);
    }

    public IndexClient Index { get; init; }

    /// <summary>
    /// This operation returns a list of all indexes in a project.
    /// </summary>
    public async Task<IndexList> ListIndexesAsync(RequestOptions? options = null)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "indexes",
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<IndexList>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    /// <summary>
    /// This operation deploys a Pinecone index. This is where you specify the measure of similarity, the dimension of vectors to be stored in the index, which cloud provider you would like to deploy with, and more.
    ///
    /// For guidance and examples, see [Create an index](https://docs.pinecone.io/guides/indexes/create-an-index#create-a-serverless-index).
    /// </summary>
    public async Task<Index> CreateIndexAsync(
        CreateIndexRequest request,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "indexes",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<Index>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 409:
                    throw new ConflictError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 422:
                    throw new UnprocessableEntityError(
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    /// <summary>
    /// Get a description of an index.
    /// </summary>
    public async Task<Index> DescribeIndexAsync(string indexName, RequestOptions? options = null)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"indexes/{indexName}",
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<Index>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    /// <summary>
    /// This operation deletes an existing index.
    /// </summary>
    public async Task DeleteIndexAsync(string indexName, RequestOptions? options = null)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"indexes/{indexName}",
                Options = options
            }
        );
        if (response.StatusCode is >= 200 and < 400)
        {
            return;
        }
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        try
        {
            switch (response.StatusCode)
            {
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 412:
                    throw new PreconditionFailedError(
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    /// <summary>
    /// This operation configures an existing index.
    ///
    /// For serverless indexes, you can configure only index deletion protection. For pod-based indexes, you can configure the pod size, number of replicas, and index deletion protection.
    ///
    /// It is not possible to change the pod type of a pod-based index. However, you can create a collection from a pod-based index and then [create a new pod-based index with a different pod type](http://docs.pinecone.io/guides/indexes/create-an-index#create-an-index-from-a-collection) from the collection.
    /// For guidance and examples, see [Configure an index](http://docs.pinecone.io/guides/indexes/configure-an-index).
    /// </summary>
    public async Task<Index> ConfigureIndexAsync(
        string indexName,
        ConfigureIndexRequest request,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethodExtensions.Patch,
                Path = $"indexes/{indexName}",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<Index>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 404:
                    throw new NotFoundError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 422:
                    throw new UnprocessableEntityError(
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    /// <summary>
    /// This operation returns a list of all collections in a project.
    /// Serverless indexes do not support collections.
    /// </summary>
    public async Task<CollectionList> ListCollectionsAsync(RequestOptions? options = null)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = "collections",
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CollectionList>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    /// <summary>
    /// This operation creates a Pinecone collection.
    ///
    /// Serverless indexes do not support collections.
    /// </summary>
    public async Task<CollectionModel> CreateCollectionAsync(
        CreateCollectionRequest request,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Post,
                Path = "collections",
                Body = request,
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CollectionModel>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 400:
                    throw new BadRequestError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 403:
                    throw new ForbiddenError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 409:
                    throw new ConflictError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
                case 422:
                    throw new UnprocessableEntityError(
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    /// <summary>
    /// This operation gets a description of a collection.
    /// Serverless indexes do not support collections.
    /// </summary>
    public async Task<CollectionModel> DescribeCollectionAsync(
        string collectionName,
        RequestOptions? options = null
    )
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Get,
                Path = $"collections/{collectionName}",
                Options = options
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            try
            {
                return JsonUtils.Deserialize<CollectionModel>(responseBody)!;
            }
            catch (JsonException e)
            {
                throw new PineconeException("Failed to deserialize response", e);
            }
        }

        try
        {
            switch (response.StatusCode)
            {
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    /// <summary>
    /// This operation deletes an existing collection.
    /// Serverless indexes do not support collections.
    /// </summary>
    public async Task DeleteCollectionAsync(string collectionName, RequestOptions? options = null)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                BaseUrl = _client.Options.BaseUrl,
                Method = HttpMethod.Delete,
                Path = $"collections/{collectionName}",
                Options = options
            }
        );
        if (response.StatusCode is >= 200 and < 400)
        {
            return;
        }
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        try
        {
            switch (response.StatusCode)
            {
                case 401:
                    throw new UnauthorizedError(JsonUtils.Deserialize<ErrorResponse>(responseBody));
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
            JsonUtils.Deserialize<object>(responseBody)
        );
    }

    private static string GetFromEnvironmentOrThrow(string env, string message)
    {
        return Environment.GetEnvironmentVariable(env) ?? throw new Exception(message);
    }
}
