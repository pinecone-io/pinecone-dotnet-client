using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

public class InferenceTests : BaseTest
{
    [Test]
    public async Task TestEmbedAsync()
    {
        // Prepare input sentences to be embedded
        var inputs = new List<EmbedRequestInputsItem>
        {
            new() { Text = "The quick brown fox jumps over the lazy dog." },
            new() { Text = "Lorem ipsum" },
        };

        // Specify the embedding model and parameters
        var embeddingModel = "multilingual-e5-large";

        // Generate embeddings for the input data
        var embeddings = await Client
            .Inference.EmbedAsync(
                new EmbedRequest
                {
                    Model = embeddingModel,
                    Inputs = inputs,
                    Parameters = new Dictionary<string, object?>
                    {
                        ["input_type"] = "query",
                        ["truncate"] = "END",
                    },
                }
            )
            .ConfigureAwait(false);

        // Verify the response contains the expected embeddings
        Assert.That(embeddings, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(embeddings.Model, Is.EqualTo(embeddingModel));
            Assert.That(embeddings.Data, Is.Not.Empty);
        });
    }

    [Test]
    public async Task TestRerankAsync()
    {
        // Prepare the query and documents to be reranked
        var query = "The tech company Apple is known for its innovative products like the iPhone.";
        var documents = new List<Dictionary<string, object?>>
        {
            new()
            {
                ["id"] = "vec1",
                ["my_field"] =
                    "Apple is a popular fruit known for its sweetness and crisp texture.",
            },
            new()
            {
                ["id"] = "vec2",
                ["my_field"] = "Many people enjoy eating apples as a healthy snack.",
            },
            new()
            {
                ["id"] = "vec3",
                ["my_field"] =
                    "Apple Inc. has revolutionized the tech industry with its sleek designs and user-friendly interfaces.",
            },
            new()
            {
                ["id"] = "vec4",
                ["my_field"] = "An apple a day keeps the doctor away, as the saying goes.",
            },
        };

        // Specify the reranking model and parameters
        var rerankModel = "bge-reranker-v2-m3";
        var rankFields = new List<string> { "my_field" };
        var topN = 2;
        var parameters = new Dictionary<string, object?> { ["truncate"] = "END" };

        // Perform the reranking
        var rerankResult = await Client
            .Inference.RerankAsync(
                new RerankRequest
                {
                    Model = rerankModel,
                    Query = query,
                    Documents = documents,
                    RankFields = rankFields,
                    TopN = topN,
                    Parameters = parameters,
                }
            )
            .ConfigureAwait(false);

        // Verify the response contains the expected reranked data
        Assert.That(rerankResult, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(rerankResult.Model, Is.EqualTo(rerankModel));
            Assert.That(rerankResult.Data, Is.Not.Empty);
        });
    }
}
