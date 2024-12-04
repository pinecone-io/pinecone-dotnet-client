namespace Pinecone;

public partial class ClientOptions
{
    /// <summary>
    /// When TLS is enabled, the client will default to using HTTPS, and when disabled, it will default to using HTTP.
    /// Defaults to true.
    /// </summary>
    public bool IsTlsEnabled { get; init; } = true;
}
