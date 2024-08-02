using NUnit.Framework;
using Pinecone.Client.Core;
using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;

namespace Pinecone.Client.Test.Wire;

[SetUpFixture]
public class GlobalTestSetup
{
    public static WireMockServer Server { get; private set; } = null!;
    public static Pinecone Client { get; private set; } = null!;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        // Start the WireMock server
        Server = WireMockServer.Start(
            new WireMockServerSettings { Logger = new WireMockConsoleLogger(), }
        );

        // Initialize the Client
        Client = new Pinecone("API_KEY", new ClientOptions { BaseUrl = Server.Urls[0] });
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        Server.Stop();
    }
}