using System.Text.Json.Serialization;
using Pinecone;
using Pinecone.Core;

namespace Pinecone.Inference;

public record ListModelsRequest
{
    /// <summary>
    /// Filter models by type ('embed' or 'rerank').
    /// </summary>
    [JsonIgnore]
    public ModelType? Type { get; set; }

    /// <summary>
    /// Filter embedding models by vector type ('dense' or 'sparse'). Only relevant when `type=embed`.
    /// </summary>
    [JsonIgnore]
    public VectorType? VectorType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
