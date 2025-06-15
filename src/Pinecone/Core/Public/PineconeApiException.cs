using Pinecone.Core;

namespace Pinecone;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class PineconeApiException : PineconeException
{
    public PineconeApiException(string message, int statusCode, ErrorResponse body)
        : this(message + (body.Error?.Message != null ? ": " + body.Error.Message : ""), statusCode, (object)body)
    { }

    public PineconeApiException(string message, int statusCode, object body) : base(message)
    {
        StatusCode = statusCode;
        Body = body;        
        Data["StatusCode"] = statusCode;
        Data["ResponseBody"] = JsonUtils.Serialize(body);
    }

    /// <summary>
    /// The error code of the response that triggered the exception.
    /// </summary>
    public int StatusCode { get; private set; }

    /// <summary>
    /// The body of the response that triggered the exception.
    /// </summary>
    public object Body { get; private set; }
}