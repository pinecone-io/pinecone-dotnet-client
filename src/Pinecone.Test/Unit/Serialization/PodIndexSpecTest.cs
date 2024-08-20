using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone;

#nullable enable

namespace Pinecone.Test;

[TestFixture]
public class PodIndexSpecTest
{
    [Test]
    public void TestSerialization()
    {
        var inputJson =
            @"
        {
  ""pod"": {
    ""environment"": ""us-east1-gcp"",
    ""replicas"": 2,
    ""shards"": 1,
    ""pod_type"": ""p1.x2"",
    ""pods"": 2,
    ""metadata_config"": {
      ""indexed"": [
        ""category"",
        ""price"",
        ""brand""
      ]
    },
    ""source_collection"": ""product-catalog""
  }
}
";

        var expectedObject = new PodIndexSpec
        {
            Pod = new PodSpec
            {
                Environment = "us-east1-gcp",
                Replicas = 2,
                Shards = 1,
                PodType = "p1.x2",
                Pods = 2,
                MetadataConfig = new PodSpecMetadataConfig
                {
                    Indexed = new List<string>() { "category", "price", "brand" }
                },
                SourceCollection = "product-catalog"
            }
        };

        var serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var deserializedObject = JsonSerializer.Deserialize<PodIndexSpec>(
            inputJson,
            serializerOptions
        );

        var serializedJson = JsonSerializer.Serialize(deserializedObject, serializerOptions);
        Assert.That(JToken.DeepEquals(JToken.Parse(inputJson), JToken.Parse(serializedJson)));
    }
}
