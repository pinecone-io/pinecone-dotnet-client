using Pinecone.Core;

#nullable enable

namespace Pinecone;

public record CancelImportResponse
{
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
