namespace Pinecone.Client.Core;

using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;

public static class ProtoConverter
{
    public static Struct ToProtoStruct(Dictionary<string, MetadataValue?> dictionary)
    {
        var protoStruct = new Struct();
        foreach (var kvp in dictionary)
        {
            protoStruct.Fields[kvp.Key] = ToProtoValue(kvp.Value);
        }
        return protoStruct;
    }

    public static Dictionary<string, MetadataValue?> FromProtoStruct(Struct protoStruct)
    {
        var dictionary = new Dictionary<string, MetadataValue?>();
        foreach (var kvp in protoStruct.Fields)
        {
            dictionary[kvp.Key] = FromProtoValue(kvp.Value);
        }
        return dictionary;
    }
    private static Value ToProtoValue(MetadataValue? metadataValue)
    {
        if (metadataValue == null)
        {
            return Value.ForNull();
        }

        return metadataValue.Match<Value>(
            Value.ForNumber,
            Value.ForString,
            Value.ForBool,
            doubleList => new Value { ListValue = new ListValue { Values = { doubleList.Select(Value.ForNumber) } } },
            stringList => new Value { ListValue = new ListValue { Values = { stringList.Select(Value.ForString) } } },
            boolList => new Value { ListValue = new ListValue { Values = { boolList.Select(Value.ForBool) } } }
        );
    }
    
    private static MetadataValue? FromProtoValue(Value value)
    {
        return value.KindCase switch
        {
            Value.KindOneofCase.NumberValue => new MetadataValue(value.NumberValue),
            Value.KindOneofCase.StringValue => new MetadataValue(value.StringValue),
            Value.KindOneofCase.BoolValue => new MetadataValue(value.BoolValue),
            Value.KindOneofCase.ListValue => FromProtoListValue(value.ListValue),
            _ => null,
        };
    }
    
    private static MetadataValue? FromProtoListValue(ListValue listValue)
    {
        if (listValue.Values.Count == 0)
        {
            return null;
        }

        var firstValue = listValue.Values[0];
        return firstValue.KindCase switch
        {
            Value.KindOneofCase.NumberValue => new MetadataValue(listValue.Values.Select(v => v.NumberValue).ToList()),
            Value.KindOneofCase.StringValue => new MetadataValue(listValue.Values.Select(v => v.StringValue).ToList()),
            Value.KindOneofCase.BoolValue => new MetadataValue(listValue.Values.Select(v => v.BoolValue).ToList()),
            _ => null,
        };
    }
}