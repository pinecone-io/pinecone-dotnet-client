using Pinecone;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class UnprocessableEntityError(ErrorResponse body)
    : PineconeApiException("UnprocessableEntityError", 422, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new ErrorResponse Body { get; } = body;
}
