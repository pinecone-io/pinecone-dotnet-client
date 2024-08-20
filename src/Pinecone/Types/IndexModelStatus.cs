using System.Text.Json.Serialization;
using Pinecone;

#nullable enable

namespace Pinecone;

public record IndexModelStatus
{
    [JsonPropertyName("ready")]
    public required bool Ready { get; set; }

    [JsonPropertyName("state")]
    public required IndexModelStatusState State { get; set; }
}
