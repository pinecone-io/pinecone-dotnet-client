using NUnit.Framework;
using Pinecone.Core;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Pinecone.Test.Unit;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class NdJsonContentTest
{
    [Test]
    public void IsRetryable_ShouldReturnFalse()
    {
        var content = new NdJsonContent(new List<object>());
        Assert.That(content.IsRetryable, Is.False);
    }

    [Test]
    public async Task SerializeToStreamAsync_ShouldSerializeEnumerableContent()
    {
        var content = new NdJsonContent(
            new List<object> { new { Name = "Test1" }, new { Name = "Test2" } }
        );
        using var stream = new MemoryStream();
        await content.CopyToAsync(stream);
        stream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(stream);
        var result = await reader.ReadToEndAsync();
        Assert.That(result, Is.EqualTo("{\"Name\":\"Test1\"}\n{\"Name\":\"Test2\"}\n"));
    }

    [Test]
    public async Task SerializeToStreamAsync_ShouldSerializeAsyncEnumerableContent()
    {
        async IAsyncEnumerable<object> GetAsyncEnumerable()
        {
            yield return new { Name = "Test1" };
            yield return new { Name = "Test2" };
        }

        var content = new NdJsonContent(GetAsyncEnumerable());
        using var stream = new MemoryStream();
        await content.CopyToAsync(stream);
        stream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(stream);
        var result = await reader.ReadToEndAsync();
        Assert.That(result, Is.EqualTo("{\"Name\":\"Test1\"}\n{\"Name\":\"Test2\"}\n"));
    }

    [Test]
    public async Task SerializeToStreamAsync_ShouldSerializeEnumeratorContent()
    {
        var list = new List<object> { new { Name = "Test1" }, new { Name = "Test2" } };
        var content = new NdJsonContent(list.GetEnumerator());
        using var stream = new MemoryStream();
        await content.CopyToAsync(stream);
        stream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(stream);
        var result = await reader.ReadToEndAsync();
        Assert.That(result, Is.EqualTo("{\"Name\":\"Test1\"}\n{\"Name\":\"Test2\"}\n"));
    }

    [Test]
    public async Task SerializeToStreamAsync_ShouldSerializeAsyncEnumeratorContent()
    {
        async IAsyncEnumerator<object> GetAsyncEnumerator()
        {
            yield return new { Name = "Test1" };
            yield return new { Name = "Test2" };
        }

        var content = new NdJsonContent(GetAsyncEnumerator());
        using var stream = new MemoryStream();
        await content.CopyToAsync(stream);
        stream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(stream);
        var result = await reader.ReadToEndAsync();
        Assert.That(result, Is.EqualTo("{\"Name\":\"Test1\"}\n{\"Name\":\"Test2\"}\n"));
    }
}
