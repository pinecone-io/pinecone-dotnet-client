using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.Test;

[TestFixture]
public class IndexTest
{
    [Test]
    public void TestSerialization()
    {
        var inputJson =
            @"
        {
  ""name"": ""movie-recommendations"",
  ""dimension"": 1536,
  ""metric"": ""cosine"",
  ""host"": ""movie-recommendations-c01b5b5.svc.us-east1-gcp.pinecone.io"",
  ""spec"": {
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
  },
  ""status"": {
    ""ready"": true,
    ""state"": ""Ready""
  }
}
";

        var expectedObject = new Index
        {
            Name = "movie-recommendations",
            Dimension = 1536,
            Metric = IndexModelMetric.Cosine,
            Host = "movie-recommendations-c01b5b5.svc.us-east1-gcp.pinecone.io",
            Spec = new IndexModelSpec
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
            },
            Status = new IndexModelStatus { Ready = true, State = IndexModelStatusState.Ready }
        };

        var serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var deserializedObject = JsonSerializer.Deserialize<Index>(inputJson, serializerOptions);
        Assert.That(expectedObject, Is.EqualTo(deserializedObject));

        var serializedJson = JsonSerializer.Serialize(deserializedObject, serializerOptions);
        Assert.That(JToken.DeepEquals(JToken.Parse(inputJson), JToken.Parse(serializedJson)));
    }
}
