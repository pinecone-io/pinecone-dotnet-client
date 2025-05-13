using System.Text.Json.Serialization;
using Pinecone.Core;

namespace Pinecone;

public record StartImportRequest
{
    /// <summary>
    /// The id of the [storage integration](https://docs.pinecone.io/guides/operations/integrations/manage-storage-integrations) that should be used to access the data.
    /// </summary>
    [JsonPropertyName("integrationId")]
    public string? IntegrationId { get; set; }

    /// <summary>
    /// The [URI prefix](https://docs.pinecone.io/guides/index-data/import-data#prepare-your-data) under which the data to import is available. All data within this prefix will be listed then imported into the target index. Currently only `s3://` URIs are supported.
    /// </summary>
    [JsonPropertyName("uri")]
    public required string Uri { get; set; }

    [JsonPropertyName("errorMode")]
    public ImportErrorMode? ErrorMode { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
