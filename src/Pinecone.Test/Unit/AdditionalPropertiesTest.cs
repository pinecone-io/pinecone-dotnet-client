using NUnit.Framework;
using Pinecone.Core;

namespace Pinecone.Test.Unit;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class AdditionalPropertiesTest
{
    [Test]
    public void TestSerialization()
    {
        const string actualJson = """
            {
              "cloud": "aws",
              "region": "us-east-1",
              "extra": "value"
            }
            """;

        var expectedObject = new ServerlessSpec
        {
            Cloud = ServerlessSpecCloud.Aws,
            Region = "us-east-1",
        };

        var actualObject = JsonUtils.Deserialize<ServerlessSpec>(actualJson);
        Assert.That(actualObject, Is.EqualTo(expectedObject).UsingDefaults());
    }
}
