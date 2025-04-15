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

        var actualObject = JsonUtils.Deserialize<ServerlessSpec>(actualJson);
        Assert.Multiple(() =>
        {
            Assert.That(actualObject.Cloud, Is.EqualTo(ServerlessSpecCloud.Aws));
            Assert.That(actualObject.Region, Is.EqualTo("us-east-1"));
            Assert.That(
                actualObject.AdditionalProperties["extra"].GetString(),
                Is.EqualTo("value")
            );
        });
    }
}
