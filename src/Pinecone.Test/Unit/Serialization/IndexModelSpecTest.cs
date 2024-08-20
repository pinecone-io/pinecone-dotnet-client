using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone;

#nullable enable

namespace Pinecone.Test;

[TestFixture]
public class IndexModelSpecTest
{
    [Test]
    public void TestSerialization()
    {
        var inputJson =
            @"
        {
  ""pod"": {
    ""environment"": ""us-east1-gcp"",
    ""replicas"": 1,
    ""shards"": 1,
    ""pod_type"": ""p1.x1"",
    ""pods"": 1,
    ""metadata_config"": {
      ""indexed"": [
        ""genre"",
        ""title"",
        ""rating""
      ]
    }
  },
  ""serverless"": {
    ""cloud"": ""aws"",
    ""region"": ""us-east-1""
  }
}
";

        var expectedObject = new IndexModelSpec
        {
            Pod = new PodSpec
            {
                Environment = "us-east1-gcp",
                Replicas = 1,
                Shards = 1,
                PodType = "p1.x1",
                Pods = 1,
                MetadataConfig = new PodSpecMetadataConfig
                {
                    Indexed = new List<string>() { "genre", "title", "rating" }
                }
            },
            Serverless = new ServerlessSpec
            {
                Cloud = ServerlessSpecCloud.Aws,
                Region = "us-east-1"
            }
        };

        var serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var deserializedObject = JsonSerializer.Deserialize<IndexModelSpec>(
            inputJson,
            serializerOptions
        );

        var serializedJson = JsonSerializer.Serialize(deserializedObject, serializerOptions);
        Assert.That(JToken.DeepEquals(JToken.Parse(inputJson), JToken.Parse(serializedJson)));
    }
}
