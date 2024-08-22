using Pinecone.Core;
using Proto = Pinecone.Grpc;

#nullable enable

namespace Pinecone;

public record DeleteResponse
{
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }

    /// <summary>
    /// Maps the DeleteResponse type into its Protobuf-equivalent representation.
    /// </summary>
    internal Proto.DeleteResponse ToProto()
    {
        return new Proto.DeleteResponse();
    }

    /// <summary>
    /// Returns a new DeleteResponse type from its Protobuf-equivalent representation.
    /// </summary>
    internal static DeleteResponse FromProto(Proto.DeleteResponse value)
    {
        return new DeleteResponse();
    }
}
