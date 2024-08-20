using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using Pinecone.Core;

namespace Pinecone.Test.Unit.Core
{
    [TestFixture]
    public class CollectionItemSerializerTests
    {
        private JsonSerializerOptions _options;

        [SetUp]
        public void SetUp()
        {
            _options = new JsonSerializerOptions();
        }

        public class TestItem
        {
            public string? Name { get; set; }
        }

        public class TestItemConverter : JsonConverter<TestItem>
        {
            public override TestItem Read(
                ref Utf8JsonReader reader,
                Type typeToConvert,
                JsonSerializerOptions options
            )
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();
                }

                var item = new TestItem();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return item;
                    }

                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string propertyName = reader.GetString()!;
                        reader.Read();

                        if (propertyName == "Name")
                        {
                            item.Name = reader.GetString();
                        }
                        else
                        {
                            reader.Skip();
                        }
                    }
                }

                throw new JsonException();
            }

            public override void Write(
                Utf8JsonWriter writer,
                TestItem value,
                JsonSerializerOptions options
            )
            {
                writer.WriteStartObject();
                writer.WriteString("Name", value.Name);
                writer.WriteEndObject();
            }
        }

        [Test]
        public void Write_ShouldSerializeCollectionToJson()
        {
            var items = new List<TestItem>
            {
                new TestItem { Name = "Item1" },
                new TestItem { Name = "Item2" }
            };
            var serializer = new CollectionItemSerializer<TestItem, TestItemConverter>();
            var stream = new MemoryStream();
            var writer = new Utf8JsonWriter(stream);

            serializer.Write(writer, items, _options);
            writer.Flush();
            var json = System.Text.Encoding.UTF8.GetString(stream.ToArray());

            Assert.That(json, Is.EqualTo("[{\"Name\":\"Item1\"},{\"Name\":\"Item2\"}]"));
        }

        [Test]
        public void Write_ShouldWriteNullForNullValue()
        {
            IEnumerable<TestItem>? items = null;
            var serializer = new CollectionItemSerializer<TestItem, TestItemConverter>();
            var stream = new MemoryStream();
            var writer = new Utf8JsonWriter(stream);

            serializer.Write(writer, items, _options);
            writer.Flush();
            var json = System.Text.Encoding.UTF8.GetString(stream.ToArray());

            Assert.That(json, Is.EqualTo("null"));
        }
    }
}
