using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Pinecone;
using Pinecone.Core;

#nullable enable

namespace Pinecone;

[JsonConverter(typeof(StringEnumSerializer<ErrorResponseErrorCode>))]
public enum ErrorResponseErrorCode
{
    [EnumMember(Value = "OK")]
    Ok,

    [EnumMember(Value = "UNKNOWN")]
    Unknown,

    [EnumMember(Value = "INVALID_ARGUMENT")]
    InvalidArgument,

    [EnumMember(Value = "DEADLINE_EXCEEDED")]
    DeadlineExceeded,

    [EnumMember(Value = "QUOTA_EXCEEDED")]
    QuotaExceeded,

    [EnumMember(Value = "NOT_FOUND")]
    NotFound,

    [EnumMember(Value = "ALREADY_EXISTS")]
    AlreadyExists,

    [EnumMember(Value = "PERMISSION_DENIED")]
    PermissionDenied,

    [EnumMember(Value = "UNAUTHENTICATED")]
    Unauthenticated,

    [EnumMember(Value = "RESOURCE_EXHAUSTED")]
    ResourceExhausted,

    [EnumMember(Value = "FAILED_PRECONDITION")]
    FailedPrecondition,

    [EnumMember(Value = "ABORTED")]
    Aborted,

    [EnumMember(Value = "OUT_OF_RANGE")]
    OutOfRange,

    [EnumMember(Value = "UNIMPLEMENTED")]
    Unimplemented,

    [EnumMember(Value = "INTERNAL")]
    Internal,

    [EnumMember(Value = "UNAVAILABLE")]
    Unavailable,

    [EnumMember(Value = "DATA_LOSS")]
    DataLoss,

    [EnumMember(Value = "FORBIDDEN")]
    Forbidden,

    [EnumMember(Value = "UNPROCESSABLE_ENTITY")]
    UnprocessableEntity
}
