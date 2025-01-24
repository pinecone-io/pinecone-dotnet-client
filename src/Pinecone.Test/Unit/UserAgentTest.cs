using NUnit.Framework;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;

namespace Pinecone.Test.Unit;

[TestFixture]
public class UserAgentTest
{
    private const string SourceTag = "test-tag";
    private const string IndexName = "mock-server-index";
    private static WireMockServer Server { get; set; } = null!;
    private static string BaseUrl { get; set; } = null!;
    private static PineconeClient Client { get; set; } = null!;
    private static IndexClient IndexClient { get; set; } = null!;
    private static RequestOptions RequestOptions { get; set; } = null!;

    [OneTimeSetUp]
    public void Setup()
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
                               },
                               "vector_type": "dense"
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
        IndexClient = Client.Index(IndexName);

        RequestOptions = new RequestOptions { BaseUrl = BaseUrl };
    }


    [OneTimeTearDown]
    public void Teardown()
    {
        Server.Stop();
        Server.Dispose();
    }

    [Test]
    public async Task ClientShouldSendUserAgent()
    {
        const string mockResponse = """
                                    {
                                      "name": "name",
                                      "dimension": 1,
                                      "metric": "cosine",
                                      "host": "host",
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
                                      },
                                      "vector_type": "dense"
                                    }
                                    """;

        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/indexes/index_name").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        await Client.DescribeIndexAsync("index_name", RequestOptions);
        var request = Server.LogEntries.Last().RequestMessage;
        var userAgent = request.Headers?["User-Agent"].ToString();
        Assert.That(userAgent, Is.Not.Null);

        const string expectedUserAgent = $"lang=C#; version={Version.Current}; source_tag={SourceTag}";
        Assert.That(userAgent, Is.EqualTo(expectedUserAgent));
    }


    [Test]
    public async Task IndexClientShouldSendUserAgent()
    {
        const string mockResponse = """
                                    {
                                      "collections": []
                                    }
                                    """;

        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/bulk/imports").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        await IndexClient.ListBulkImportsAsync(new ListBulkImportsRequest());

        var request = Server.LogEntries.Last().RequestMessage;
        var userAgent = request.Headers?["User-Agent"].ToString();
        Assert.That(userAgent, Is.Not.Null);

        const string expectedUserAgent = $"lang=C#; version={Version.Current}; source_tag={SourceTag}";
        Assert.That(userAgent, Is.EqualTo(expectedUserAgent));
    }
}