using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class RerankTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
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
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<RerankResult>(mockResponse)).UsingDefaults()
        );
    }
}
