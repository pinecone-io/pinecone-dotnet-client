using System.Threading.Tasks;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

#nullable enable

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class RerankTest : BaseMockServerTest
{
    [Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "model": "model",
              "query": "query",
              "top_n": 1,
              "return_documents": true,
              "rank_fields": [
                "rank_fields",
                "rank_fields"
              ],
              "documents": [
                {
                  "documents": "documents"
                },
                {
                  "documents": "documents"
                }
              ],
              "parameters": {
                "parameters": "parameters"
              }
            }
            """;

        const string mockResponse = """
            {
              "model": "model",
              "data": [
                {
                  "index": 1,
                  "score": 1.1,
                  "document": {
                    "document": "document"
                  }
                },
                {
                  "index": 1,
                  "score": 1.1,
                  "document": {
                    "document": "document"
                  }
                }
              ],
              "usage": {
                "rerank_units": 1
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/rerank")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Inference.RerankAsync(
            new RerankRequest
            {
                Model = "model",
                Query = "query",
                TopN = 1,
                ReturnDocuments = true,
                RankFields = new List<string>() { "rank_fields", "rank_fields" },
                Documents = new List<Dictionary<string, string>>()
                {
                    new Dictionary<string, string>() { { "documents", "documents" } },
                    new Dictionary<string, string>() { { "documents", "documents" } },
                },
                Parameters = new Dictionary<string, string>() { { "parameters", "parameters" } },
            },
            RequestOptions
        );
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }

    [Test]
    public async Task MockServerTest_2()
    {
        const string requestJson = """
            {
              "model": "bge-reranker-v2-m3",
              "query": "What is the capital of France?",
              "documents": [
                {
                  "id": "1",
                  "text": "Paris is the capital of France.",
                  "title": "France",
                  "url": "https://example.com"
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "model": "bge-reranker-v2-m3",
              "data": [
                {
                  "index": 1,
                  "score": 0.5,
                  "document": {
                    "id": "1",
                    "text": "Paris is the capital of France.",
                    "title": "France",
                    "url": "https://example.com"
                  }
                }
              ],
              "usage": {
                "rerank_units": 1
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/rerank")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Inference.RerankAsync(
            new RerankRequest
            {
                Model = "bge-reranker-v2-m3",
                Query = "What is the capital of France?",
                Documents = new List<Dictionary<string, string>>()
                {
                    new Dictionary<string, string>()
                    {
                        { "id", "1" },
                        { "text", "Paris is the capital of France." },
                        { "title", "France" },
                        { "url", "https://example.com" },
                    },
                },
            },
            RequestOptions
        );
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }
}
