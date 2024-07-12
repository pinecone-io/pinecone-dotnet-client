using System.Net.Http;
using System.Text.Json;
using Pinecone.Client.ControlPlane;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public class ControlPlaneClient
{
    private RawClient _client;

    public ControlPlaneClient(RawClient client)
    {
        _client = client;
        Inference = new InferenceClient(_client);
    }

    public InferenceClient Inference { get; }

    /// <summary>
    /// This operation returns a list of all indexes in a project.
    /// </summary>
    public async Task<IndexList> ListIndexesAsync()
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = "indexes" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<IndexList>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// This operation deploys a Pinecone index. This is where you specify the measure of similarity, the dimension of vectors to be stored in the index, which cloud provider you would like to deploy with, and more.
    ///
    /// For guidance and examples, see [Create an index](https://docs.pinecone.io/guides/indexes/create-an-index#create-a-serverless-index).
    /// </summary>
    public async Task<Index> CreateIndexAsync(CreateIndexRequest request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "indexes",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<Index>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// Get a description of an index.
    /// </summary>
    public async Task<Index> DescribeIndexAsync(string indexName)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = $"indexes/{indexName}" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<Index>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// This operation deletes an existing index.
    /// </summary>
    public async Task DeleteIndexAsync(string indexName)
    {
        await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"indexes/{indexName}"
            }
        );
    }

    /// <summary>
    /// This operation specifies the pod type and number of replicas for an index. It applies to pod-based indexes only. Serverless indexes scale automatically based on usage.
    /// </summary>
    public async Task ConfigureIndexAsync(string indexName, ConfigureIndexRequest request)
    {
        await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethodExtensions.Patch,
                Path = $"indexes/{indexName}",
                Body = request
            }
        );
    }

    /// <summary>
    /// This operation returns a list of all collections in a project.
    /// Serverless indexes do not support collections.
    /// </summary>
    public async Task<CollectionList> ListCollectionsAsync()
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest { Method = HttpMethod.Get, Path = "collections" }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CollectionList>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// This operation creates a Pinecone collection.
    ///
    /// Serverless indexes do not support collections.
    /// </summary>
    public async Task<CollectionModel> CreateCollectionAsync(CreateCollectionRequest request)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Post,
                Path = "collections",
                Body = request
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CollectionModel>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// This operation gets a description of a collection.
    /// Serverless indexes do not support collections.
    /// </summary>
    public async Task<CollectionModel> DescribeCollectionAsync(string collectionName)
    {
        var response = await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Get,
                Path = $"collections/{collectionName}"
            }
        );
        var responseBody = await response.Raw.Content.ReadAsStringAsync();
        if (response.StatusCode is >= 200 and < 400)
        {
            return JsonSerializer.Deserialize<CollectionModel>(responseBody)!;
        }
        throw new Exception(responseBody);
    }

    /// <summary>
    /// This operation deletes an existing collection.
    /// Serverless indexes do not support collections.
    /// </summary>
    public async Task DeleteCollectionAsync(string collectionName)
    {
        await _client.MakeRequestAsync(
            new RawClient.JsonApiRequest
            {
                Method = HttpMethod.Delete,
                Path = $"collections/{collectionName}"
            }
        );
    }
}
