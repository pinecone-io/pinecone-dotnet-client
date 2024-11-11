using System.Threading.Tasks;
using FluentAssertions.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone;
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
              "parameters": {
                "input_type": "input_type",
                "truncate": "truncate"
              },
              "inputs": [
                {
                  "text": "text"
                },
                {
                  "text": "text"
                }
              ]
            }
            """;

        const string mockResponse = """
            {
              "model": "model",
              "data": [
                {
                  "values": [
                    1.1,
                    1.1
                  ]
                },
                {
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
                Parameters = new EmbedRequestParameters
                {
                    InputType = "input_type",
                    Truncate = "truncate",
                },
                Inputs = new List<EmbedRequestInputsItem>()
                {
                    new EmbedRequestInputsItem { Text = "text" },
                    new EmbedRequestInputsItem { Text = "text" },
                },
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
              "model": "multilingual-e5-large",
              "inputs": [
                {}
              ]
            }
            """;

        const string mockResponse = """
            {
              "model": "multilingual-e5-large",
              "data": [
                {
                  "values": [
                    0.1,
                    0.2,
                    0.3
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
        JToken
            .Parse(mockResponse)
            .Should()
            .BeEquivalentTo(JToken.Parse(JsonUtils.Serialize(response)));
    }
}
