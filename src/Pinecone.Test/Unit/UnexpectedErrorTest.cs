using NUnit.Framework;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;

namespace Pinecone.Test.Unit;

[TestFixture]
public class UnexpectedErrorTest
{
    private static WireMockServer Server { get; set; } = null!;
    private static string BaseUrl { get; set; } = null!;
    private static PineconeClient Client { get; set; } = null!;
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
            BaseUrl = BaseUrl
        });

        RequestOptions = new RequestOptions { BaseUrl = BaseUrl };
    }


    [OneTimeTearDown]
    public void Teardown()
    {
        Server.Stop();
        Server.Dispose();
    }

    [Test]
    public void ShouldThrowPineconeApiException()
    {
        Server
            .Given(
                WireMock.RequestBuilders.Request.Create().WithPath("/indexes/index_name").UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(504)
                    .WithBody("504 Gateway Timeout")
            );

        var exception = Assert.ThrowsAsync<PineconeApiException>(
            async () => await Client.DescribeIndexAsync("index_name", RequestOptions).ConfigureAwait(false)
        );
        Assert.Multiple(() =>
        {
            Assert.That(exception.StatusCode, Is.EqualTo(504));
            Assert.That(exception.Body, Is.InstanceOf<string>());
            Assert.That(exception.Body.ToString(), Is.EqualTo("504 Gateway Timeout"));
        });
    }
}