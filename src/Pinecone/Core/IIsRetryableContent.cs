namespace Pinecone.Core;

public interface IIsRetryableContent
{
    public bool IsRetryable { get; }
}
