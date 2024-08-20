using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone;

#nullable enable

namespace Pinecone.Test;

[TestFixture]
public class CollectionModelTest
{
    [Test]
    public void TestSerialization()
    {
        var inputJson =
            @"
        {
  ""name"": ""example-collection"",
  ""size"": 10000000,
  ""status"": ""Ready"",
  ""dimension"": 1536,
  ""vector_count"": 120000,
  ""environment"": ""us-east1-gcp""
}
";

        var expectedObject = new CollectionModel
        {
            Name = "example-collection",
            Size = 10000000,
            Status = CollectionModelStatus.Ready,
            Dimension = 1536,
            VectorCount = 120000,
            Environment = "us-east1-gcp"
        };

        var serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var deserializedObject = JsonSerializer.Deserialize<CollectionModel>(
            inputJson,
            serializerOptions
        );

        var serializedJson = JsonSerializer.Serialize(deserializedObject, serializerOptions);
        Assert.That(JToken.DeepEquals(JToken.Parse(inputJson), JToken.Parse(serializedJson)));
    }
}
