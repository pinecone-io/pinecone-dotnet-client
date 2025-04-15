using NUnit.Framework;
using Pinecone.Core;

#nullable enable

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class EmbedTest : BaseMockServerTest
{
    [Test]
    public async Task MockServerTest_1()
    {
        const string requestJson = """
            {
              "model": "model",
              "inputs": [
                {},
                {}
              ]
            }
            """;

        const string mockResponse = """
            {
              "model": "model",
              "vector_type": "dense",
              "data": [
                {
                  "vector_type": "dense",
                  "values": [
                    1.1,
                    1.1
                  ]
                },
                {
                  "vector_type": "dense",
                  "values": [
                    1.1,
                    1.1
                  ]
                }
              ],
              "usage": {
                "total_tokens": 1
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/embed")
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

        var response = await Client.Inference.EmbedAsync(
            new EmbedRequest
            {
                Model = "model",
                Parameters = null,
                Inputs = new List<EmbedRequestInputsItem>()
                {
                    new EmbedRequestInputsItem { Text = null },
                    new EmbedRequestInputsItem { Text = null },
                },
            },
            RequestOptions
        );

        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<EmbeddingsList>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async Task MockServerTest_2()
    {
        const string requestJson = """
            {
              "model": "multilingual-e5-large",
              "inputs": [
                {}
              ]
            }
            """;

        const string mockResponse = """
            {
              "model": "multilingual-e5-large",
              "vector_type": "sparse",
              "data": [
                {
                  "vector_type": "sparse",
                  "sparse_values": [
                    1.1
                  ],
                  "sparse_indices": [
                    1
                  ],
                  "sparse_tokens": [
                    "sparse_tokens"
                  ]
                }
              ],
              "usage": {
                "total_tokens": 205
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/embed")
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

        var response = await Client.Inference.EmbedAsync(
            new EmbedRequest
            {
                Model = "multilingual-e5-large",
                Inputs = new List<EmbedRequestInputsItem>() { new EmbedRequestInputsItem() },
            },
            RequestOptions
        );

        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<EmbeddingsList>(mockResponse)).UsingDefaults()
        );
    }
}
