namespace Pinecone;

/// <summary>
/// This exception type will be thrown for any non-2XX API responses.
/// </summary>
public class PineconeApiException : PineconeException
{
    public PineconeApiException(string message, int statusCode, object body) : base(message)
    {
        StatusCode = statusCode;
        Body = body;        
        Data["StatusCode"] = statusCode;
        Data["ResponseBody"] = body;
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