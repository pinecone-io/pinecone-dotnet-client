namespace Pinecone.Test.Integration.Control;

public abstract class BaseTest
{
    protected PineconeClient Client => Setup.Client;
    protected string IndexName => Setup.IndexName;
    protected string Environment => Setup.PineconeEnvironment;
    protected int Dimension => Setup.Dimension;
    protected CreateIndexRequestMetric Metric => Setup.Metric;
    protected string CollectionName => Setup.CollectionName;
}
