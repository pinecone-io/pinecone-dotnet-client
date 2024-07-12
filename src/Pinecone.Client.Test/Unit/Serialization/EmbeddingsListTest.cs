using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client.Test;

[TestFixture]
public class EmbeddingsListTest
{
    [Test]
    public void TestSerialization()
    {
        var inputJson =
            @"
        {
  ""model"": ""multilingual-e5-large"",
  ""data"": [
    {
      ""values"": [
        0.1,
        0.2,
        0.3
      ]
    }
  ],
  ""usage"": {
    ""total_tokens"": 205
  }
}
";

        var expectedObject = new EmbeddingsList
        {
            Model = "multilingual-e5-large",
            Data = new List<Embedding>()
            {
                new Embedding
                {
                    Values = new List<double>() { 0.1, 0.2, 0.3 }
                }
            },
            Usage = new EmbeddingsListUsage { TotalTokens = 205 }
        };

        var serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var deserializedObject = JsonSerializer.Deserialize<EmbeddingsList>(
            inputJson,
            serializerOptions
        );
        Assert.That(expectedObject, Is.EqualTo(deserializedObject));

        var serializedJson = JsonSerializer.Serialize(deserializedObject, serializerOptions);
        Assert.That(JToken.DeepEquals(JToken.Parse(inputJson), JToken.Parse(serializedJson)));
    }
}
