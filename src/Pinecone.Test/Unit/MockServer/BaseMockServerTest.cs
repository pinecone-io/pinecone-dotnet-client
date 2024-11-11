using NUnit.Framework;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;

namespace Pinecone.Test.Unit.MockServer;

[SetUpFixture]
public class BaseMockServerTest
{
    protected const string SourceTag = "test-tag";
    private const string IndexName = "mock-server-index";
    protected static WireMockServer Server { get; set; } = null!;

    private static string BaseUrl { get; set; } = null!;

    protected static PineconeClient Client { get; set; } = null!;

    protected static IndexClient IndexClient { get; set; } = null!;

    protected static RequestOptions RequestOptions { get; set; } = null!;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        // Start the WireMock server
        Server = WireMockServer.Start(
            new WireMockServerSettings { Logger = new WireMockConsoleLogger() }
        );
        BaseUrl = Server.Urls[0];

        // Initialize the Client
        Client = new PineconeClient("API_KEY", new ClientOptions
        {
            SourceTag = SourceTag,
            BaseUrl = BaseUrl
        });
        SetUpIndexClientMock();
        IndexClient = Client.Index(IndexName);

        RequestOptions = new RequestOptions { BaseUrl = BaseUrl };
    }

    private void SetUpIndexClientMock()
    {
        var mockResponse = $$"""
                             {
                               "name": "{{IndexName}}",
                               "dimension": 1,
                               "metric": "cosine",
                               "host": "{{BaseUrl}}",
                               "deletion_protection": "disabled",
                               "tags": {
                                 "tags": "tags"
                               },
                               "spec": {
                                 "serverless": {
                                   "cloud": "gcp",
                                   "region": "region"
                                 }
                               },
                               "status": {
                                 "ready": true,
                                 "state": "Initializing"
                               }
                             }
                             """;

        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath($"/indexes/{IndexName}").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );
    }
    
    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        Server.Stop();
    }
}