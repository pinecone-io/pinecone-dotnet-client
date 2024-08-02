using HandlebarsDotNet.Helpers.Utils;
using NUnit.Framework;
using Pinecone.Client.Test.Utils;
using WireMock.Server;
using JsonUtils = Pinecone.Client.Core.JsonUtils;

namespace Pinecone.Client.Test.Wire;

[TestFixture]
public class CreateIndexTest
{
    private Pinecone _client;
    private WireMockServer _server;

    [SetUp]
    public void SetUp()
    {
        _client = GlobalTestSetup.Client;
        _server = GlobalTestSetup.Server;
    }

    [TearDown]
    public void TearDown()
    {
        // Reset the WireMock server after each test
        GlobalTestSetup.Server.Reset();
    }

    [Test]
    public void TestCreateIndex()
    {
        var inputJson = """
                        {
                          "name": "serverless-index",
                          "dimension": 1536,
                          "metric": "cosine",
                          "deletion_protection": null,
                          "spec": {
                            "cloud": "aws",
                            "region": "us-east-1"
                          }
                        }
                        """;
        
        var mockResponse = """
                               {
                                 "name": "serverless-index",
                                 "dimension": 3,
                                 "metric": "cosine",
                                 "host": "serverless-index-gb6vrs7.svc.aped-4627-b74a.pinecone.io",
                                 "deletion_protection": "disabled",
                                 "spec": {
                                   "pod": null,
                                   "serverless": {
                                     "cloud": "aws",
                                     "region": "us-east-1"
                                   }
                                 },
                                 "status": {
                                   "ready": false,
                                   "state": "Initializing"
                                 }
                               }
                               """;
        _server.Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/indexes")
                    .UsingPost()
                    .WithBody(inputJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );
        
        var request = new CreateIndexRequest
        {
            Name = "serverless-index",
            Dimension = 1536,
            Metric = CreateIndexRequestMetric.Cosine,
            Spec = new ServerlessSpec
            {
                Cloud = ServerlessSpecCloud.Aws,
                Region = "us-east-1"
            }
        };
        var response = _client.CreateIndexAsync(request).Result;
        
        JsonDiffChecker.AssertJsonEquals(mockResponse, JsonUtils.Serialize(response));
    }
}