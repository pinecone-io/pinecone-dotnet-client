using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Pinecone.Core;

namespace Pinecone;

public partial class RestoreJobsClient
{
    private RawClient _client;

    internal RestoreJobsClient(RawClient client)
    {
        _client = client;
    }

    /// <summary>
    /// List all restore jobs for a project.
    /// </summary>
    /// <example><code>
    /// await client.RestoreJobs.ListAsync(new ListRestoreJobsRequest());
    /// </code></example>
    public async Task<RestoreJobList> ListAsync(
        ListRestoreJobsRequest request,
        RequestOptions? options = null,
        CancellationToken cancellationToken = default
    )
    {
        var _query = new Dictionary<string, object>();
        if (request.Limit != null)
        {
            _query["limit"] = request.Limit.Value.ToString();
        }
        if (request.PaginationToken != null)
        {
            _query["paginationToken"] = request.PaginationToken;
        }
        var response = await _client
            .SendRequestAsync(
                new JsonRequest
                {
                    BaseUrl = _client.Options.BaseUrl,
                    Method = HttpMethod.Get,
                    Path = "restore-jobs",
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
                return JsonUtils.Deserialize<RestoreJobList>(responseBody)!;
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
    /// Get a description of a restore job.
    /// </summary>
    /// <example><code>
    /// await client.RestoreJobs.GetAsync("670e8400-e29b-41d4-a716-446655440000");
    /// </code></example>
    public async Task<RestoreJobModel> GetAsync(
        string jobId,
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
                        "restore-jobs/{0}",
                        ValueConvert.ToPathParameterString(jobId)
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
                return JsonUtils.Deserialize<RestoreJobModel>(responseBody)!;
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
