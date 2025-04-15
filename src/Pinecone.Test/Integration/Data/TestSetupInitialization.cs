using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

public class TestSetupInitialization : BaseTest
{
    [Test]
    public async Task TestIndexDirectHostKwarg()
    {
        var index = Client.Index(host: IndexHost);
        var results = await index
            .FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } })
            .ConfigureAwait(false);
        Assert.That(results, Is.Not.Null);
    }

    [Test]
    public async Task TestIndexDirectHostWithHttps()
    {
        var index = Client.Index(
            host: IndexHost.StartsWith("https://") ? IndexHost : "https://" + IndexHost
        );
        var results = await index
            .FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } })
            .ConfigureAwait(false);
        Assert.That(results, Is.Not.Null);
    }

    [Test]
    public async Task TestIndexDirectHostWithoutHttps()
    {
        var index = Client.Index(
            host: IndexHost.StartsWith("https://") ? IndexHost.Substring(8) : IndexHost
        );
        var results = await index
            .FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } })
            .ConfigureAwait(false);
        Assert.That(results, Is.Not.Null);
    }

    [Test]
    public async Task TestIndexByNamePositionalOnly()
    {
        var index = Client.Index(name: IndexName);
        var results = await index
            .FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } })
            .ConfigureAwait(false);
        Assert.That(results, Is.Not.Null);
    }

    [Test]
    public async Task TestIndexByNamePositionalWithHost()
    {
        var index = Client.Index(name: IndexName, host: IndexHost);
        var results = await index
            .FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } })
            .ConfigureAwait(false);
        Assert.That(results, Is.Not.Null);
    }

    [Test]
    public async Task TestIndexByNameKwargs()
    {
        var index = Client.Index(name: IndexName);
        var results = await index
            .FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } })
            .ConfigureAwait(false);
        Assert.That(results, Is.Not.Null);
    }

    [Test]
    public async Task TestIndexByNameKwargsWithHost()
    {
        var index = Client.Index(name: IndexName, host: IndexHost);
        var results = await index
            .FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } })
            .ConfigureAwait(false);
        Assert.That(results, Is.Not.Null);
    }
}
