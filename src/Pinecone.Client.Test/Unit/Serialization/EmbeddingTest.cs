using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.Test;

[TestFixture]
public class EmbeddingTest
{
    [Test]
    public void TestSerialization()
    {
        var inputJson =
            @"
        {
  ""values"": [
    0.1,
    0.2,
    0.3
  ]
}
";

        var expectedObject = new Embedding
        {
            Values = new List<double>() { 0.1, 0.2, 0.3 }
        };

        var serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var deserializedObject = JsonSerializer.Deserialize<Embedding>(
            inputJson,
            serializerOptions
        );
        Assert.That(expectedObject, Is.EqualTo(deserializedObject));

        var serializedJson = JsonSerializer.Serialize(deserializedObject, serializerOptions);
        Assert.That(JToken.DeepEquals(JToken.Parse(inputJson), JToken.Parse(serializedJson)));
    }
}
