using NUnit.Framework;

namespace Pinecone.Test.Integration.Control
{
    [SetUpFixture]
    public class Setup
    {
        public static PineconeClient Client { get; private set; } = null!;
        public static string IndexName { get; private set; } = null!;
        public static string PineconeEnvironment { get; private set; } = null!;
        public static int Dimension { get; private set; }
        public static CreateIndexRequestMetric Metric { get; private set; }
        public static string CollectionName { get; private set; } = null!;
        public static string Host { get; private set; } = null!;

        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            Console.WriteLine("Initializing control plane integration tests...");
            Client = new PineconeClient(apiKey: Helpers.GetEnvironmentVar("PINECONE_API_KEY"));
            PineconeEnvironment = "us-west1-gcp";
            Dimension = 2;
            Metric = CreateIndexRequestMetric.Cosine;
            IndexName = Helpers.GenerateIndexName("global-index");

            Host = await Helpers.CreatePodIndexAndWaitUntilReady(
                Client,
                IndexName,
                PineconeEnvironment,
                Dimension,
                Metric
            );

            CollectionName = $"reused-coll-{Helpers.RandomString(10)}";
            await CreateReusableCollection(CollectionName, Dimension);
            Console.WriteLine("Control plane integration test intialization complete.");
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

            Console.WriteLine($"Attempting to upsert vectors to index {IndexName}");
            var index = Client.Index(IndexName);
            await index.UpsertAsync(new UpsertRequest { Vectors = vectors });

            Console.WriteLine($"Attempting to create collection with name {collectionName} from index {IndexName}");
            await Helpers.CreateCollectionAndWaitUntilReady(Client, collectionName, IndexName);
            Console.WriteLine($"Collection {collectionName} created successfully!");

            return collectionName;
        }
    }
}
