using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.Test;

[TestFixture]
public class PodSpecMetadataConfigTest
{
    [Test]
    public void TestSerialization()
    {
        var inputJson =
            @"
        {
  ""indexed"": [
    ""genre"",
    ""title"",
    ""release_year"",
    ""director"",
    ""actor""
  ]
}
";

        var expectedObject = new PodSpecMetadataConfig
        {
            Indexed = new List<string>() { "genre", "title", "release_year", "director", "actor" }
        };

        var serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var deserializedObject = JsonSerializer.Deserialize<PodSpecMetadataConfig>(
            inputJson,
            serializerOptions
        );
        Assert.That(expectedObject, Is.EqualTo(deserializedObject));

        var serializedJson = JsonSerializer.Serialize(deserializedObject, serializerOptions);
        Assert.That(JToken.DeepEquals(JToken.Parse(inputJson), JToken.Parse(serializedJson)));
    }
}
