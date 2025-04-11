using System.Net.Http;
using System.Text.Json;
using Pinecone.Core;

namespace Pinecone.VectorOperations;

public partial class RecordsClient
{
    /// <summary>
    /// This operation converts input data to vector embeddings and then upserts the embeddings into a namespace.
    /// </summary>
    /// <example><code>
    /// await client.VectorOperations.Records.UpsertNamespaceAsync(
    ///     "namespace",
    ///     new List&lt;UpsertRecord&gt;() { new UpsertRecord { Id = "example-record-1" } }
    /// );
    /// </code></example>
    public Task UpsertNamespaceAsync(
        string @namespace,
        IEnumerable<UpsertRecord> request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => UpsertNamespaceAsyncInternal(@namespace, request, options, cancellationToken);

    /// <summary>
    /// This operation converts input data to vector embeddings and then upserts the embeddings into a namespace.
    /// </summary>
    /// <example><code>
    /// await client.VectorOperations.Records.UpsertNamespaceAsync(
    ///     "namespace",
    ///     enumerator
    /// );
    /// </code></example>
    public Task UpsertNamespaceAsync(
        string @namespace,
        IEnumerator<UpsertRecord> request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => UpsertNamespaceAsyncInternal(@namespace, request, options, cancellationToken);

    /// <summary>
    /// This operation converts input data to vector embeddings and then upserts the embeddings into a namespace.
    /// </summary>
    /// <example><code>
    /// await client.VectorOperations.Records.UpsertNamespaceAsync(
    ///     "namespace",
    ///     records
    /// );
    /// </code></example>
    public Task UpsertNamespaceAsync(
        string @namespace,
        IAsyncEnumerable<UpsertRecord> request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => UpsertNamespaceAsyncInternal(@namespace, request, options, cancellationToken);

    /// <summary>
    /// This operation converts input data to vector embeddings and then upserts the embeddings into a namespace.
    /// </summary>
    /// <example><code>
    /// await client.VectorOperations.Records.UpsertNamespaceAsync(
    ///     "namespace",
    ///     enumerator
    /// );
    /// </code></example>
    public Task UpsertNamespaceAsync(
        string @namespace,
        IAsyncEnumerator<UpsertRecord> request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    ) => UpsertNamespaceAsyncInternal(@namespace, request, options, cancellationToken);

    private async Task UpsertNamespaceAsyncInternal(
        string @namespace,
        object request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var response = await _client
            .SendRequestAsync(
                new NdJsonRequest
                {
                    Body = request,
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Post,
                    Path = string.Format(
                        "records/namespaces/{0}/upsert",
                        ValueConvert.ToPathParameterString(@namespace)
                    ),
                    ContentType = "application/x-ndjson",
                    Options = options,
                },
                cancellationToken
            )
            .ConfigureAwait(false);
        if (response.StatusCode is >= 200 and < 400)
        {
            return;
        }

        {
#if NET6_0_OR_GREATER
            var responseBody = await response
                .Raw.Content.ReadAsStringAsync(cancellationToken)
                .ConfigureAwait(false);
#else
            var responseBody = await response.Raw.Content.ReadAsStringAsync().ConfigureAwait(false);
#endif
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
