using System.Text.Json.Serialization;
using Pinecone.Client;

#nullable enable

namespace Pinecone.Client;

public record IndexModelStatus
{
    [JsonPropertyName("ready")]
    public required bool Ready { get; init; }

    [JsonPropertyName("state")]
    public required IndexModelStatusState State { get; init; }
}
