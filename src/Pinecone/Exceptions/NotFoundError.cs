namespace Pinecone;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class NotFoundError(ErrorResponse body) : PineconeApiException("NotFoundError", 404, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new ErrorResponse Body => body;
}
