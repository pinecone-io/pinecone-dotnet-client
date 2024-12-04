using NUnit.Framework;
using Pinecone;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;

#nullable enable

namespace Pinecone.Test.Unit.MockServer;

[SetUpFixture]
public class BaseMockServerTest
{
    protected static WireMockServer Server { get; set; } = null!;

    protected static BasePinecone Client { get; set; } = null!;

    protected static RequestOptions RequestOptions { get; set; } = null!;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        // Start the WireMock server
        Server = WireMockServer.Start(
            new WireMockServerSettings { Logger = new WireMockConsoleLogger() }
        );

        // Initialize the Client
        Client = new BasePinecone("API_KEY");

        RequestOptions = new RequestOptions { BaseUrl = Server.Urls[0] };
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        Server.Stop();
        Server.Dispose();
    }
}
