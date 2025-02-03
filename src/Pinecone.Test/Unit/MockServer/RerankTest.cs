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
              "documents": [
                {
                  "documents": {
                    "key": "value"
                  }
                },
                {
                  "documents": {
                    "key": "value"
                  }
                }
              ]
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
                    "document": {
                      "key": "value"
                    }
                  }
                },
                {
                  "index": 1,
                  "score": 1.1,
                  "document": {
                    "document": {
                      "key": "value"
                    }
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
                    .WithHeader("Content-Type", "application/json")
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
                TopN = null,
                ReturnDocuments = null,
                RankFields = null,
                Documents = new List<Dictionary<string, object?>>()
                {
                    new Dictionary<string, object>()
                    {
                        {
                            "documents",
                            new Dictionary<object, object?>() { { "key", "value" } }
                        },
                    },
                    new Dictionary<string, object>()
                    {
                        {
                            "documents",
                            new Dictionary<object, object?>() { { "key", "value" } }
                        },
                    },
                },
                Parameters = null,
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
                    .WithHeader("Content-Type", "application/json")
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
                Documents = new List<Dictionary<string, object?>>()
                {
                    new Dictionary<string, object>()
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
