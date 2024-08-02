using Pinecone.Client;
using Pinecone.Client.Core;

#nullable enable

namespace Pinecone.Client;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class PreconditionFailedError(ErrorResponse body)
    : PineconeApiException("PreconditionFailedError", 412, body)
{
    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public new ErrorResponse Body { get; } = body;
}
