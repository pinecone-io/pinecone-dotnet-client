using NUnit.Framework;
using Pinecone.Test.Wire;

namespace Pinecone.Test.Integration;

public class BaseDataPlaneTest
{
    protected string _apiKey => DataPlaneTestSetup.ApiKey;
    protected PineconeClient _client => DataPlaneTestSetup.Client;
    protected string _indexHost => DataPlaneTestSetup.IndexHost;
    protected string _indexName => DataPlaneTestSetup.IndexName;
    protected string _listNamespace => DataPlaneTestSetup.ListNamespace;
    protected CreateIndexRequestMetric _metric => DataPlaneTestSetup.Metric;
    protected string _namespace => DataPlaneTestSetup.Namespace;
    protected ServerlessIndexSpec _spec => DataPlaneTestSetup.Spec;
    protected string _weirdIdsNamespace => DataPlaneTestSetup.WeirdIdsNamespace;
    protected IndexClient _indexClient => DataPlaneTestSetup.IndexClient;
    
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        DataPlaneTestSetup.Initialize();
    }
}