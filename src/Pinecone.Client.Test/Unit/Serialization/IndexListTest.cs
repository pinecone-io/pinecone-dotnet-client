using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.Test;

[TestFixture]
public class IndexListTest
{
    [Test]
    public void TestSerialization()
    {
        var inputJson =
            @"
        {
  ""indexes"": [
    {
      ""name"": ""example-index"",
      ""dimension"": 1536,
      ""metric"": ""cosine"",
      ""host"": ""example-index-c01b5b5.svc.us-west1-gcp.pinecone.io"",
      ""spec"": {
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
  ]
}
";

        var expectedObject = new IndexList
        {
            Indexes = new List<Index>()
            {
                new Index
                {
                    Name = "example-index",
                    Dimension = 1536,
                    Metric = IndexModelMetric.Cosine,
                    Host = "example-index-c01b5b5.svc.us-west1-gcp.pinecone.io",
                    Spec = new IndexModelSpec
                    {
                        Serverless = new ServerlessSpec
                        {
                            Cloud = ServerlessSpecCloud.Aws,
                            Region = "us-east-1"
                        }
                    },
                    Status = new IndexModelStatus
                    {
                        Ready = true,
                        State = IndexModelStatusState.Ready
                    }
                }
            }
        };

        var serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var deserializedObject = JsonSerializer.Deserialize<IndexList>(
            inputJson,
            serializerOptions
        );
        Assert.That(expectedObject, Is.EqualTo(deserializedObject));

        var serializedJson = JsonSerializer.Serialize(deserializedObject, serializerOptions);
        Assert.That(JToken.DeepEquals(JToken.Parse(inputJson), JToken.Parse(serializedJson)));
    }
}
