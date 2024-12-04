namespace Pinecone;

public partial class ClientOptions
{
    /// <summary>
    /// When TLS is enabled, the client will use the HTTPS protocol. When disabled, the client will use the HTTP protocol.
    /// Defaults to true.
    /// </summary>
    public bool IsTlsEnabled { get; init; } = true;
}
