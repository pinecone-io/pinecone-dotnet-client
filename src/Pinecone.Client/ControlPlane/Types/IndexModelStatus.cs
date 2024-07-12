using System.Text.Json.Serialization;
using Pinecone.Client.ControlPlane;

#nullable enable

namespace Pinecone.Client.ControlPlane;

public record IndexModelStatus
{
    [JsonPropertyName("ready")]
    public required bool Ready { get; init; }

    [JsonPropertyName("state")]
    public required IndexModelStatusState State { get; init; }
}
