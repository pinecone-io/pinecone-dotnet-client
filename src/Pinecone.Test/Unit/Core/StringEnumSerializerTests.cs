using System.Runtime.Serialization;
using System.Text.Json;
using NUnit.Framework;
using Pinecone.Core;

namespace Pinecone.Test.Unit.Core
{
    public enum TestEnum
    {
        [EnumMember(Value = "Value_One")]
        ValueOne,

        [EnumMember(Value = "Value_Two")]
        ValueTwo,

        [EnumMember(Value = "Value_Three")]
        ValueThree
    }

    [TestFixture]
    public class StringEnumSerializerTests
    {
        private JsonSerializerOptions _options;

        [SetUp]
        public void SetUp()
        {
            _options = new JsonSerializerOptions();
            _options.Converters.Add(new StringEnumSerializer<TestEnum>());
        }

        [Test]
        public void Write_ShouldSerializeEnumToString()
        {
            var value = TestEnum.ValueOne;
            var expectedJson = "\"Value_One\"";

            var json = JsonSerializer.Serialize(value, _options);

            Assert.That(json, Is.EqualTo(expectedJson));
        }

        [Test]
        public void Read_ShouldDeserializeStringToEnum()
        {
            var json = "\"Value_Two\"";
            var expectedValue = TestEnum.ValueTwo;

            var result = JsonSerializer.Deserialize<TestEnum>(json, _options);

            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test]
        public void Read_ShouldReturnDefault_ForUnknownString()
        {
            var json = "\"Unknown_Value\"";
            var expectedValue = default(TestEnum);

            var result = JsonSerializer.Deserialize<TestEnum>(json, _options);

            Assert.That(result, Is.EqualTo(expectedValue));
        }
    }
}
