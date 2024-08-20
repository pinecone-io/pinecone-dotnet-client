using System;

#nullable enable

namespace Pinecone.Core;

/// <summary>
/// Base exception class for all exceptions thrown by the SDK.
/// </summary>
public class PineconeException(string message, Exception? innerException = null)
    : Exception(message, innerException) { }
