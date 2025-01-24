using NUnit.Framework;
using Pinecone.Test.Integration.Data;

namespace Pinecone.Test.Integration;

[Parallelizable(ParallelScope.Children)]
public class BaseTest
{
    protected PineconeClient Client => Setup.Client;
    protected string IndexHost => Setup.IndexHost;
    protected string IndexName => Setup.IndexName;
    protected string ListNamespace => Setup.ListNamespace;
    protected string Namespace => Setup.Namespace;
    protected IndexClient IndexClient => Setup.IndexClient;
}
