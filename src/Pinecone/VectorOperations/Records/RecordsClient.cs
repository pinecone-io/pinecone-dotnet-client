using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.VectorOperations;

public partial class RecordsClient
{
    private RawClient _client;

    internal RecordsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// This operation converts a query to a vector embedding and then searches a namespace using the embedding. It returns the most similar records in the namespace, along with their similarity scores.
    /// </summary>
    /// <example><code>
    /// await client.VectorOperations.Records.SearchNamespaceAsync(
    ///     "namespace",
    ///     new SearchRecordsNamespaceRequest
    ///     {
    ///         Query = new SearchRecordsRequestQuery
    ///         {
    ///             TopK = 10,
    ///             Inputs = new Dictionary&lt;string, object&gt;() { { "text", "your query text" } },
    ///         },
    ///         Fields = new List&lt;string&gt;() { "chunk_text" },
    ///     }
    /// );
    /// </code></example>
    public async Task<SearchRecordsResponse> SearchNamespaceAsync(
        string namespace_,
        SearchRecordsNamespaceRequest request,
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
                    Path = string.Format(
                        "records/namespaces/{0}/search",
                        ValueConvert.ToPathParameterString(namespace_)
                    ),
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
                return JsonUtils.Deserialize<SearchRecordsResponse>(responseBody)!;
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
                        throw new BadRequestError(JsonUtils.Deserialize<object>(responseBody));
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
