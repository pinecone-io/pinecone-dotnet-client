using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client.Test;

[TestFixture]
public class ServerlessIndexSpecTest
{
    [Test]
    public void TestSerialization()
    {
        var inputJson =
            @"
        {
  ""serverless"": {
    ""cloud"": ""aws"",
    ""region"": ""us-east-1""
  }
}
";

        var expectedObject = new ServerlessIndexSpec
        {
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

        var deserializedObject = JsonSerializer.Deserialize<ServerlessIndexSpec>(
            inputJson,
            serializerOptions
        );
        Assert.That(expectedObject, Is.EqualTo(deserializedObject));

        var serializedJson = JsonSerializer.Serialize(deserializedObject, serializerOptions);
        Assert.That(JToken.DeepEquals(JToken.Parse(inputJson), JToken.Parse(serializedJson)));
    }
}
