using System.Text.Json.Serialization;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record ListImportsResponse
{
    [JsonPropertyName("data")]
    public IEnumerable<ImportModel>? Data { get; set; }

    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
