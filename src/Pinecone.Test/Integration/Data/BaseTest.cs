using NUnit.Framework;
using Pinecone.Test.Integration.Data;

namespace Pinecone.Test.Integration;

public class BaseTest
{
    protected string _apiKey => Setup.ApiKey;
    protected PineconeClient _client => Setup.Client;
    protected string _indexHost => Setup.IndexHost;
    protected string _indexName => Setup.IndexName;
    protected string _listNamespace => Setup.ListNamespace;
    protected CreateIndexRequestMetric _metric => Setup.Metric;
    protected string _namespace => Setup.Namespace;
    protected ServerlessIndexSpec _spec => Setup.Spec;
    protected IndexClient _indexClient => Setup.IndexClient;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        Setup.Initialize();
    }
}
