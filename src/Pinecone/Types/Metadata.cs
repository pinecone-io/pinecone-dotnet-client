using Proto = Google.Protobuf.WellKnownTypes;

#nullable enable

namespace Pinecone;

public sealed class Metadata : Dictionary<string, MetadataValue?>
{
    public Metadata() { }

    public Metadata(IEnumerable<KeyValuePair<string, MetadataValue?>> value)
        : base(value.ToDictionary(e => e.Key, e => e.Value)) { }

    internal Proto.Struct ToProto()
    {
        var result = new Proto.Struct();
        foreach (var kvp in this)
        {
            result.Fields[kvp.Key] = kvp.Value?.ToProto();
        }
        return result;
    }

    internal static Metadata FromProto(Proto.Struct value)
    {
        var result = new Metadata();
        foreach (var kvp in value.Fields)
        {
            result[kvp.Key] = kvp.Value != null ? MetadataValue.FromProto(kvp.Value) : null;
        }
        return result;
    }
}
