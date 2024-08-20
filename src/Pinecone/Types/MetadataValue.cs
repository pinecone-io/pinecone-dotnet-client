using System.Text.Json.Serialization;
using OneOf;
using Pinecone.Core;

namespace Pinecone;

[JsonConverter(typeof(OneOfSerializer<MetadataValue>))]
public class MetadataValue(
    OneOf<
        string,
        double,
        bool,
        IEnumerable<MetadataValue?>,
        Dictionary<string, MetadataValue?>
    > value
)
    : OneOfBase<
        string,
        double,
        bool,
        IEnumerable<MetadataValue?>,
        Dictionary<string, MetadataValue?>
    >(value)
{
    public static implicit operator MetadataValue(string value)
    {
        return new MetadataValue(value);
    }

    public static implicit operator MetadataValue(bool value)
    {
        return new MetadataValue(value);
    }

    public static implicit operator MetadataValue(double value)
    {
        return new MetadataValue(value);
    }

    public static implicit operator MetadataValue(Dictionary<string, MetadataValue?> value)
    {
        return new MetadataValue(value);
    }

    public static implicit operator MetadataValue(MetadataValue?[] value)
    {
        return new MetadataValue(value);
    }

    public static implicit operator MetadataValue(List<MetadataValue?> value)
    {
        return new MetadataValue(value);
    }

    public static implicit operator MetadataValue(string[] value)
    {
        return new MetadataValue(
            value.Select(v => v != null ? new MetadataValue(v) : null).ToList()
        );
    }

    public static implicit operator MetadataValue(double[] value)
    {
        return new MetadataValue(value.Select(v => new MetadataValue(v)).ToList());
    }

    public static implicit operator MetadataValue(double?[] value)
    {
        return new MetadataValue(
            value.Select(v => v != null ? new MetadataValue(v.Value) : null).ToList()
        );
    }

    public static implicit operator MetadataValue(bool[] value)
    {
        return new MetadataValue(value.Select(v => new MetadataValue(v)).ToList());
    }

    public static implicit operator MetadataValue(bool?[] value)
    {
        return new MetadataValue(
            value.Select(v => v != null ? new MetadataValue(v.Value) : null).ToList()
        );
    }

    public static implicit operator MetadataValue(List<string> value)
    {
        return new MetadataValue(value.Select(v => new MetadataValue(v)).ToList());
    }

    public static implicit operator MetadataValue(List<double> value)
    {
        return new MetadataValue(value.Select(v => new MetadataValue(v)).ToList());
    }

    public static implicit operator MetadataValue(List<double?> value)
    {
        return new MetadataValue(
            value.Select(v => v != null ? new MetadataValue(v.Value) : null).ToList()
        );
    }

    public static implicit operator MetadataValue(List<bool> value)
    {
        return new MetadataValue(value.Select(v => new MetadataValue(v)).ToList());
    }

    public static implicit operator MetadataValue(List<bool?> value)
    {
        return new MetadataValue(
            value.Select(v => v != null ? new MetadataValue(v.Value) : null).ToList()
        );
    }
}
