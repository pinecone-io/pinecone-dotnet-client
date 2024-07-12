using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client.Test;

[TestFixture]
public class CollectionListTest
{
    [Test]
    public void TestSerialization()
    {
        var inputJson =
            @"
        {
  ""collections"": [
    {
      ""name"": ""movie-embeddings"",
      ""size"": 10000000,
      ""status"": ""Ready"",
      ""dimension"": 1536,
      ""vector_count"": 100000,
      ""environment"": ""us-east1-gcp""
    },
    {
      ""name"": ""product-catalog"",
      ""size"": 5000000,
      ""status"": ""Initializing"",
      ""dimension"": 768,
      ""vector_count"": 50000,
      ""environment"": ""us-west1-gcp""
    }
  ]
}
";

        var expectedObject = new CollectionList
        {
            Collections = new List<CollectionModel>()
            {
                new CollectionModel
                {
                    Name = "movie-embeddings",
                    Size = 10000000,
                    Status = CollectionModelStatus.Ready,
                    Dimension = 1536,
                    VectorCount = 100000,
                    Environment = "us-east1-gcp"
                },
                new CollectionModel
                {
                    Name = "product-catalog",
                    Size = 5000000,
                    Status = CollectionModelStatus.Initializing,
                    Dimension = 768,
                    VectorCount = 50000,
                    Environment = "us-west1-gcp"
                }
            }
        };

        var serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var deserializedObject = JsonSerializer.Deserialize<CollectionList>(
            inputJson,
            serializerOptions
        );
        Assert.That(expectedObject, Is.EqualTo(deserializedObject));

        var serializedJson = JsonSerializer.Serialize(deserializedObject, serializerOptions);
        Assert.That(JToken.DeepEquals(JToken.Parse(inputJson), JToken.Parse(serializedJson)));
    }
}
