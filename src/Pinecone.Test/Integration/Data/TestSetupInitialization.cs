using NUnit.Framework;

namespace Pinecone.Test.Integration;

public class TestSetupInitialization : BaseTest
{
    [Test]
    public async Task TestIndexDirectHostKwarg()
    {
        var index = _client.Index(host: _indexHost);
        var results = await index.FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } });
        Assert.That(results, Is.Not.Null);
    }

    [Test]
    public async Task TestIndexDirectHostWithHttps()
    {
        var index = _client.Index(
            host: _indexHost.StartsWith("https://") ? _indexHost : "https://" + _indexHost
        );
        var results = await index.FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } });
        Assert.That(results, Is.Not.Null);
    }

    [Test]
    public async Task TestIndexDirectHostWithoutHttps()
    {
        var index = _client.Index(
            host: _indexHost.StartsWith("https://") ? _indexHost.Substring(8) : _indexHost
        );
        var results = await index.FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } });
        Assert.That(results, Is.Not.Null);
    }

    [Test]
    public async Task TestIndexByNamePositionalOnly()
    {
        var index = _client.Index(name: _indexName);
        var results = await index.FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } });
        Assert.That(results, Is.Not.Null);
    }

    [Test]
    public async Task TestIndexByNamePositionalWithHost()
    {
        var index = _client.Index(name: _indexName, host: _indexHost);
        var results = await index.FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } });
        Assert.That(results, Is.Not.Null);
    }

    [Test]
    public async Task TestIndexByNameKwargs()
    {
        var index = _client.Index(name: _indexName);
        var results = await index.FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } });
        Assert.That(results, Is.Not.Null);
    }

    [Test]
    public async Task TestIndexByNameKwargsWithHost()
    {
        var index = _client.Index(name: _indexName, host: _indexHost);
        var results = await index.FetchAsync(new FetchRequest { Ids = new[] { "1", "2", "3" } });
        Assert.That(results, Is.Not.Null);
    }
}
