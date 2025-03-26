using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;
using Pinecone.VectorOperations;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class SearchNamespaceTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "query": {
                "top_k": 10,
                "inputs": {
                  "text": "your query text"
                }
              },
              "fields": [
                "chunk_text"
              ]
            }
            """;

        const string mockResponse = """
            {
              "result": {
                "hits": [
                  {
                    "_id": "example-record-1",
                    "_score": 0.9281134605407715,
                    "fields": {
                      "data": "your example text"
                    }
                  }
                ]
              },
              "usage": {
                "read_units": 5,
                "embed_total_tokens": 10,
                "rerank_units": 1
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/records/namespaces/namespace/search")
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

        var response = await Client.VectorOperations.Records.SearchNamespaceAsync(
            "namespace",
            new SearchRecordsNamespaceRequest
            {
                Query = new SearchRecordsRequestQuery
                {
                    TopK = 10,
                    Inputs = new Dictionary<string, object>() { { "text", "your query text" } },
                },
                Fields = new List<string>() { "chunk_text" },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<SearchRecordsResponse>(mockResponse)).UsingDefaults()
        );
    }
}
