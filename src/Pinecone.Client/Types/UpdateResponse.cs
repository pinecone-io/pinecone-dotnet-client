using System.Text.Json.Serialization;
using Proto = Pinecone.Grpc;

namespace Pinecone.Client;

public record UpdateResponse
{
    #region Mappers

    public static UpdateResponse FromProto(Proto.UpdateResponse proto)
    {
        return new UpdateResponse { };
    }

    #endregion
}
