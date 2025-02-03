using Grpc.Core;
using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

public class TestSetupFetch : BaseTest
{
    private const int ExpectedDimension = 2;

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestFetchMultipleById(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var results = await IndexClient.FetchAsync(
            new FetchRequest { Ids = new[] { "1", "2", "4" }, Namespace = targetNamespace }
        ).ConfigureAwait(false);

        Assert.That(results, Is.InstanceOf<FetchResponse>());
        Assert.That(results.Usage, Is.Not.Null);
        Assert.That(results.Usage.ReadUnits, Is.Not.Null);
        Assert.That(results.Usage.ReadUnits, Is.GreaterThan(0));

        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Vectors, Has.Count.EqualTo(3));

        Assert.That(results.Vectors["1"].Id, Is.EqualTo("1"));
        Assert.That(results.Vectors["2"].Id, Is.EqualTo("2"));

        Assert.That(results.Vectors["1"].Metadata, Is.Null);
        Assert.That(results.Vectors["2"].Metadata, Is.Null);

        Assert.That(results.Vectors["4"].Metadata, Is.Not.Null);
        Assert.That(
            results.Vectors["4"].Metadata["genre"],
            Is.EqualTo(new MetadataValue("action"))
        );
        Assert.That(results.Vectors["4"].Metadata["runtime"], Is.EqualTo(new MetadataValue(120)));

        Assert.That(results.Vectors["1"].Values!.Value.Length, Is.EqualTo(ExpectedDimension));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestFetchSingleById(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var results = await IndexClient.FetchAsync(
            new FetchRequest { Ids = new[] { "1" }, Namespace = targetNamespace }
        ).ConfigureAwait(false);

        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Vectors, Has.Count.EqualTo(1));

        Assert.That(results.Vectors["1"].Id, Is.EqualTo("1"));
        Assert.That(results.Vectors["1"].Metadata, Is.Null);
        Assert.That(results.Vectors["1"].Values.Value.Length, Is.EqualTo(ExpectedDimension));
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task TestFetchNonexistentId(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var results = await IndexClient.FetchAsync(
            new FetchRequest { Ids = new[] { "100" }, Namespace = targetNamespace }
        ).ConfigureAwait(false);

        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Vectors!, Is.Empty);
    }

    [Test]
    public async Task TestFetchNonexistentNamespace()
    {
        var targetNamespace = "nonexistent-namespace";

        var results = await IndexClient.FetchAsync(
            new FetchRequest { Ids = new[] { "1" }, Namespace = targetNamespace }
        ).ConfigureAwait(false);

        Assert.That(results.Namespace, Is.EqualTo(targetNamespace));
        Assert.That(results.Vectors!, Is.Empty);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void TestFetchWithEmptyListOfIds(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var exception = Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.FetchAsync(
                new FetchRequest { Ids = Array.Empty<string>(), Namespace = targetNamespace }
            ).ConfigureAwait(false);
        });

        Assert.That(exception.StatusCode, Is.EqualTo(3));
    }

    [Test]
    public async Task TestFetchUnspecifiedNamespace()
    {
        var results = await IndexClient.FetchAsync(new FetchRequest { Ids = new[] { "1", "4" } }).ConfigureAwait(false);

        Assert.That(results.Namespace, Is.EqualTo(""));
        Assert.That(results.Vectors?["1"].Id, Is.EqualTo("1"));
        Assert.That(results.Vectors?["1"].Values, Is.Not.Null);
        Assert.That(results.Vectors?["4"].Metadata, Is.Not.Null);
    }
}
