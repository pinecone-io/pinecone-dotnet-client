using Grpc.Core;
using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

public class TestSetupQueryErrorCases : BaseTest
{
    [TestCase(true)]
    [TestCase(false)]
    public void TestQueryWithInvalidVector(bool useNondefaultNamespace)
    {
        var targetNamespace = useNondefaultNamespace ? Namespace : "";

        var ex = Assert.ThrowsAsync<PineconeApiException>(async () =>
        {
            await IndexClient.QueryAsync(
                new QueryRequest
                {
                    Vector = new float[] { 1, 2, 3 }, // Invalid vector size
                    Namespace = targetNamespace,
                    TopK = 10
                }
            ).ConfigureAwait(false);
        });

        Assert.That(ex.StatusCode, Is.EqualTo(3));
    }
}
