using global::System.Threading.Tasks;
using NUnit.Framework;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Test.Unit.MockServer;

[TestFixture]
public class GetTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_1()
    {
        const string mockResponse = """
            {
              "name": "example-embedding-model",
              "short_description": "An example embedding model.",
              "type": "embed",
              "vector_type": "dense",
              "dimension": 768,
              "modality": "text",
              "sequence_length": 512,
              "batch_size": 96,
              "supported_metrics": [
                "cosine",
                "euclidean"
              ],
              "supported_parameters": [
                {
                  "parameter": "example_required_param",
                  "type": "one_of",
                  "value_type": "string",
                  "required": true,
                  "allowed_values": [
                    "value1",
                    "value2"
                  ],
                  "min": 1,
                  "max": 1,
                  "default": "default"
                },
                {
                  "parameter": "example_param_with_default",
                  "type": "one_of",
                  "value_type": "string",
                  "required": false,
                  "allowed_values": [
                    "value1",
                    "value2"
                  ],
                  "min": 1,
                  "max": 1,
                  "default": "value1"
                },
                {
                  "parameter": "example_numeric_range",
                  "type": "numeric_range",
                  "value_type": "integer",
                  "required": false,
                  "allowed_values": [
                    "allowed_values"
                  ],
                  "min": 0,
                  "max": 10,
                  "default": 5
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/models/multilingual-e5-large")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Inference.Models.GetAsync("multilingual-e5-large");
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ModelInfo>(mockResponse)).UsingDefaults()
        );
    }

    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest_2()
    {
        const string mockResponse = """
            {
              "name": "example-reranking-model",
              "short_description": "An example reranking model.",
              "type": "rerank",
              "vector_type": "dense",
              "dimension": 1024,
              "modality": "text",
              "sequence_length": 1024,
              "batch_size": 100,
              "supported_metrics": [
                "cosine"
              ],
              "supported_parameters": [
                {
                  "parameter": "example_any_value",
                  "type": "any",
                  "value_type": "boolean",
                  "required": false,
                  "allowed_values": [
                    "allowed_values"
                  ],
                  "min": 1,
                  "max": 1,
                  "default": true
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/models/multilingual-e5-large")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Inference.Models.GetAsync("multilingual-e5-large");
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ModelInfo>(mockResponse)).UsingDefaults()
        );
    }
}
