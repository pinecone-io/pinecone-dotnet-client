using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record ImportErrorMode
{
    /// <summary>
    /// Indicates how to respond to errors during the import process.
    /// </summary>
    [JsonPropertyName("onError")]
    public ImportErrorModeOnError? OnError { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
