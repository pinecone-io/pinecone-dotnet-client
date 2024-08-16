using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

namespace Pinecone.Client;

public record DeleteResponse
{
    #region Mappers

    public static DeleteResponse FromProto(Proto.DeleteResponse proto)
    {
        return new DeleteResponse { };
    }

    #endregion
}
