using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using OneOf;
using Pinecone.Client.Core;

namespace Pinecone.Client.Test.Unit.Core
{
    public class OneOfExample : OneOfBase<OneOfExample.Option1, OneOfExample.Option2>
    {
        public class Option1
        {
            public required string Value { get; set; }
        }

        public class Option2
        {
            public required int Number { get; set; }
        }

        public OneOfExample(OneOf<Option1, Option2> input)
            : base(input) { }
    }

    public class Option1Converter : JsonConverter<OneOfExample.Option1>
    {
        public override OneOfExample.Option1 Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            reader.Read();
            var value = reader.GetString();
            reader.Read();
            return new OneOfExample.Option1 { Value = value! };
        }

        public override void Write(
            Utf8JsonWriter writer,
            OneOfExample.Option1 value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStartObject();
            writer.WriteString("Value", value.Value);
            writer.WriteEndObject();
        }
    }

    public class Option2Converter : JsonConverter<OneOfExample.Option2>
    {
        public override OneOfExample.Option2 Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            reader.Read();
            var number = reader.GetInt32();
            reader.Read();
            return new OneOfExample.Option2 { Number = number };
        }

        public override void Write(
            Utf8JsonWriter writer,
            OneOfExample.Option2 value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStartObject();
            writer.WriteNumber("Number", value.Number);
            writer.WriteEndObject();
        }
    }

    [TestFixture]
    public class OneOfSerializerTests
    {
        private JsonSerializerOptions _options;

        [SetUp]
        public void SetUp()
        {
            _options = new JsonSerializerOptions();
            _options.Converters.Add(new Option1Converter());
            _options.Converters.Add(new Option2Converter());
            _options.Converters.Add(new OneOfSerializer<OneOfExample>());
        }

        [Test]
        public void Write_ShouldSerializeOption1()
        {
            var option = new OneOfExample.Option1 { Value = "test" };
            var oneOf = new OneOfExample(option);
            var serializer = new OneOfSerializer<OneOfExample>();
            var stream = new MemoryStream();
            var writer = new Utf8JsonWriter(stream);

            serializer.Write(writer, oneOf, _options);
            writer.Flush();
            var json = System.Text.Encoding.UTF8.GetString(stream.ToArray());

            Assert.That(json, Is.EqualTo("{\"Value\":\"test\"}"));
        }

        [Test]
        public void Write_ShouldSerializeOption2()
        {
            var option = new OneOfExample.Option2 { Number = 42 };
            var oneOf = new OneOfExample(option);
            var serializer = new OneOfSerializer<OneOfExample>();
            var stream = new MemoryStream();
            var writer = new Utf8JsonWriter(stream);

            serializer.Write(writer, oneOf, _options);
            writer.Flush();
            var json = System.Text.Encoding.UTF8.GetString(stream.ToArray());

            Assert.That(json, Is.EqualTo("{\"Number\":42}"));
        }
    }
}
