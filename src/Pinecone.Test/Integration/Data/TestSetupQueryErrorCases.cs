using Grpc.Core;
using NUnit.Framework;
using Pinecone.Core;

namespace Pinecone.Test.Integration;

public class TestSetupQueryErrorCases : BaseTest
{
    [TestCase(true)]
    [TestCase(false)]
    public void TestQueryWithInvalidVector(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? _namespace : "";

        var ex = Assert.ThrowsAsync<RpcException>(async () =>
        {
            await _indexClient.QueryAsync(
                new QueryRequest
                {
                    Vector = new float[] { 1, 2, 3 }, // Invalid vector size
                    Namespace = targetNamespace,
                    TopK = 10
                }
            );
        });

        Assert.That(
            ex.Message.Contains("vector", StringComparison.CurrentCultureIgnoreCase),
            Is.True
        );
    }
}
