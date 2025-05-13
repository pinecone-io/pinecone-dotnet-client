using NUnit.Framework;

namespace Pinecone.Test.Integration.Control
{
    [SetUpFixture]
    [Parallelizable(ParallelScope.Children)]
    public class Setup
    {
        public static PineconeClient Client { get; private set; } = null!;
        public static string IndexName { get; private set; } = null!;
        public static string PineconeEnvironment { get; private set; } = null!;
        public static int Dimension { get; private set; }
        public static MetricType Metric { get; private set; }
        public static string CollectionName { get; private set; } = null!;
        public static string Host { get; private set; } = null!;

        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            await TestContext
                .Out.WriteLineAsync("Initializing control plane integration tests...")
                .ConfigureAwait(false);
            Client = new PineconeClient(
                apiKey: Helpers.GetEnvironmentVar("PINECONE_API_KEY"),
                new ClientOptions { SourceTag = "test-tag" }
            );
            PineconeEnvironment = "us-west1-gcp";
            Dimension = 2;
            Metric = MetricType.Cosine;
            IndexName = Helpers.GenerateIndexName("global-index");

            Host = await Helpers
                .CreatePodIndexAndWaitUntilReady(
                    Client,
                    IndexName,
                    PineconeEnvironment,
                    Dimension,
                    Metric
                )
                .ConfigureAwait(false);

            CollectionName = $"reused-coll-{Helpers.RandomString(10)}";
            await CreateReusableCollection(CollectionName, Dimension).ConfigureAwait(false);
            await TestContext
                .Out.WriteLineAsync("Control plane integration test intialization complete.")
                .ConfigureAwait(false);
        }

        [OneTimeTearDown]
        public async Task GlobalCleanup()
        {
            await Helpers.Cleanup(Client).ConfigureAwait(false);
        }

        private async Task<string> CreateReusableCollection(string collectionName, int dimension)
        {
            var numVectors = 10;
            var vectors = Enumerable
                .Range(0, numVectors)
                .Select(i => new Vector
                {
                    Id = i.ToString(),
                    Values = Helpers.EmbeddingValues(dimension),
                })
                .ToList();

            await TestContext
                .Out.WriteLineAsync($"Attempting to upsert vectors to index {IndexName}")
                .ConfigureAwait(false);
            var index = Client.Index(IndexName);
            await index.UpsertAsync(new UpsertRequest { Vectors = vectors }).ConfigureAwait(false);

            await TestContext
                .Out.WriteLineAsync(
                    $"Attempting to create collection with name {collectionName} from index {IndexName}"
                )
                .ConfigureAwait(false);
            await Helpers
                .CreateCollectionAndWaitUntilReady(Client, collectionName, IndexName)
                .ConfigureAwait(false);
            await TestContext
                .Out.WriteLineAsync($"Collection {collectionName} created successfully!")
                .ConfigureAwait(false);

            return collectionName;
        }
    }
}
