using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone;

#nullable enable

namespace Pinecone.Test.Integration.Serialization;

[TestFixture]
public class AdditionalPropertiesTest
{
    [Test]
    public void TestSerialization()
    {
        var inputJson =
            @"
        {
  ""cloud"": ""aws"",
  ""region"": ""us-east-1"",
  ""extra"": ""value""
}
";

        var expectedJson =
            @"
        {
  ""cloud"": ""aws"",
  ""region"": ""us-east-1""
}
";

        var expectedObject = new ServerlessSpec
        {
            Cloud = ServerlessSpecCloud.Aws,
            Region = "us-east-1"
        };

        var serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var deserializedObject = JsonSerializer.Deserialize<ServerlessSpec>(
            inputJson,
            serializerOptions
        );

        var serializedJson = JsonSerializer.Serialize(deserializedObject, serializerOptions);
        Assert.That(JToken.DeepEquals(JToken.Parse(expectedJson), JToken.Parse(serializedJson)));
    }
}
