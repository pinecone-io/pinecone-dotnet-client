using System.Net.Http;

namespace Pinecone.Client.Core;

public static class HttpMethodExtensions
{
    public static readonly HttpMethod Patch = new("PATCH");
}
