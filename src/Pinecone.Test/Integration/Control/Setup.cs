using NUnit.Framework;

namespace Pinecone.Test.Integration.Control
{
    [SetUpFixture]
    public class Setup
    {
        public static PineconeClient Client { get; private set; }
        public static string IndexName { get; private set; }
        public static string PineconeEnvironment { get; private set; }
        public static int Dimension { get; private set; }
        public static CreateIndexRequestMetric Metric { get; private set; }
        public static string CollectionName { get; private set; }

        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            Client = new PineconeClient(apiKey: Helpers.GetEnvironmentVar("PINECONE_API_KEY"));
            PineconeEnvironment = "us-west1-gcp";
            Dimension = 2;
            Metric = CreateIndexRequestMetric.Cosine;
            IndexName = Helpers.GenerateIndexName("global-index");

            await Helpers.CreatePodIndexAndWaitUntilReady(
                Client,
                IndexName,
                PineconeEnvironment,
                Dimension,
                Metric
            );
            await Task.Delay(10000); // Extra wait, since status is sometimes inaccurate

            CollectionName = $"reused-coll-{Helpers.RandomString(10)}";
            await CreateReusableCollection(CollectionName, Dimension);
        }

        [OneTimeTearDown]
        public async Task GlobalCleanup()
        {
            await Helpers.Cleanup(Client);
        }

        private async Task<string> CreateReusableCollection(string collectionName, int dimension)
        {
            var numVectors = 10;
            var vectors = Enumerable
                .Range(0, numVectors)
                .Select(i => new Vector
                {
                    Id = i.ToString(),
                    Values = Helpers.EmbeddingValues(dimension)
                })
                .ToList();

            var index = Client.Index(IndexName);
            await index.UpsertAsync(new UpsertRequest { Vectors = vectors });

            await Helpers.CreateCollectionAndWaitUntilReady(Client, collectionName, IndexName);

            return collectionName;
        }
    }
}
