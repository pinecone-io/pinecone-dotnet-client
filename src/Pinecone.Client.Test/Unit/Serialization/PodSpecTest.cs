using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client.Test;

[TestFixture]
public class PodSpecTest
{
    [Test]
    public void TestSerialization()
    {
        var inputJson =
            @"
        {
  ""environment"": ""us-east1-gcp"",
  ""replicas"": 1,
  ""shards"": 1,
  ""pod_type"": ""p1.x1"",
  ""pods"": 1,
  ""metadata_config"": {
    ""indexed"": [
      ""genre"",
      ""title"",
      ""imdb_rating""
    ]
  },
  ""source_collection"": ""movie-embeddings""
}
";

        var expectedObject = new PodSpec
        {
            Environment = "us-east1-gcp",
            Replicas = 1,
            Shards = 1,
            PodType = "p1.x1",
            Pods = 1,
            MetadataConfig = new PodSpecMetadataConfig
            {
                Indexed = new List<string>() { "genre", "title", "imdb_rating" }
            },
            SourceCollection = "movie-embeddings"
        };

        var serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var deserializedObject = JsonSerializer.Deserialize<PodSpec>(inputJson, serializerOptions);

        var serializedJson = JsonSerializer.Serialize(deserializedObject, serializerOptions);
        Assert.That(JToken.DeepEquals(JToken.Parse(inputJson), JToken.Parse(serializedJson)));
    }
}
