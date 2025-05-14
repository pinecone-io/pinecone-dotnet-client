using NUnit.Framework;

namespace Pinecone.Test.Integration.Control;

[Parallelizable(ParallelScope.All)]
public abstract class BaseTest
{
    protected PineconeClient Client => Setup.Client;
    protected string IndexName => Setup.IndexName;
    protected string Environment => Setup.PineconeEnvironment;
    protected int Dimension => Setup.Dimension;
    protected MetricType Metric => Setup.Metric;
    protected string CollectionName => Setup.CollectionName;
}
