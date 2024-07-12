# Pinecone .NET Library

[![fern shield](https://img.shields.io/badge/%F0%9F%8C%BF-SDK%20generated%20by%20Fern-brightgreen)](https://github.com/fern-api/fern)
![NuGet Version](https://img.shields.io/nuget/v/Pinecone.Net)

The official Pinecone C# library, supporting .NET Standard, .NET Core, and .NET Framework.

## Installation

Using the .NET Core command-line interface (CLI) tools:

```sh
dotnet add package Pinecone.Client
```

Using the NuGet Command Line Interface (CLI):

```sh
nuget install Pinecone.Client
```

## Documentation

API reference documentation is available [here](https://docs.pinecone.io/reference/api/introduction).

## Usage

Below are code snippets of how you can use the C# SDK.

### Create Index

```csharp
using Pinecone.Client;


var pinecone = new Pinecone("YOUR_API_KEY")

var createIndexRequest = new CreateIndexRequest
{
    Name = "serverless-index",
    Dimension = 1536,
    Metric = CreateIndexRequestMetric.Cosine,
    Spec = new ServerlessIndexSpec
    {
        Serverless = new ServerlessSpec
        {
            Cloud = ServerlessSpecCloud.Aws,
            Region = "us-east-1"
        }
    }
};

var indexModel = pinecone.ControlPlane.CreateIndexAsync(request: createIndexRequest).Result;
```

### Create Collection

```csharp
using Pinecone.Client;


var pinecone = new Pinecone("YOUR_API_KEY")

var createCollectionRequest = new CreateCollectionRequest
{
    Name = "my-collection",
    Source = "pod-index"
};

var collectionModel = pinecone
    .ControlPlane.CreateCollectionAsync(request: createCollectionRequest)
    .Result;
```

### Configure Pod

```csharp
using Pinecone.Client;


var pinecone = new Pinecone("YOUR_API_KEY")

var configureIndexRequest = new ConfigureIndexRequest
{
    Spec = new ConfigureIndexRequestSpec
    {
        Pod = new ConfigureIndexRequestSpecPod { Replicas = 2, PodType = "p1.x1" }
    }
};

var index = _client
    .ControlPlane.ConfigureIndexAsync("pod-index", configureIndexRequest)
    .Result;
```

## Advanced

### Retries

429 Rate Limit, and >=500 Internal errors will all be
retried twice with exponential backoff. You can override this behavior
globally or per-request.

```csharp
var pinecone = new Pinecone("...", new ClientOptions {
    MaxRetries = 1 // Only retry once
});
```

### Timeouts

The SDK defaults to a 60s timeout. You can override this behaviour
globally or per-request.

```csharp
var pinecone = new Pinecone("...", new ClientOptions {
    TimeoutInSeconds = 20 // Lower timeout
});
```

### HTTP Client

You can override the HttpClient by passing in `ClientOptions`.

```csharp
var pinecone = new Pinecone("YOUR_API_KEY", new ClientOptions {
    HttpClient = ... // Override the Http Client
    BaseURL = ... // Override the Base URL
})
```

## Contributing

While we value open-source contributions to this SDK, this library
is generated programmatically. Additions made directly to this library
would have to be moved over to our generation code, otherwise they would
be overwritten upon the next generated release. Feel free to open a PR as a
proof of concept, but know that we will not be able to Pinecone it as-is.
We suggest opening an issue first to discuss with us!

On the other hand, contributions to the README are always very welcome!
