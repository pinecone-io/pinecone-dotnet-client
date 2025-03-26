using Pinecone.Core;

namespace Pinecone.VectorOperations;

public partial class VectorOperationsClient
{
    private RawClient _client;

    internal VectorOperationsClient(RawClient client)
    {
        _client = client;
        Records = new RecordsClient(_client);
    }

    public RecordsClient Records { get; }
}
