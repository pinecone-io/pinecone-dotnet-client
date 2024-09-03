# Pinecone .NET Library

[![NuGet](https://img.shields.io/nuget/v/Pinecone.Client.svg)](https://www.nuget.org/packages/Pinecone.Client)
[![fern shield](https://img.shields.io/badge/%F0%9F%8C%BF-SDK%20generated%20by%20Fern-brightgreen)](https://buildwithfern.com/?utm_campaign=pinecone-io/pinecone-dotnet-client&utm_medium=readme&utm_source=github)

The official Pinecone .NET library supporting .NET Standard, .NET Core, and .NET Framework.

## Requirements

To use this SDK, ensure that your project is targeting one of the following:

* .NET Standard 2.0+
* .NET Core 3.0+
* .NET Framework 4.6.2+
* .NET 6.0+

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

Instantiate the SDK using the `Pinecone` class.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY")
```

## Indexes

Operations related to the building and managing of Pinecone indexes are called
[control plane](https://docs.pinecone.io/reference/api/introduction#control-plane)
operations.

### Create index

You can use the .NET SDK to create two types of indexes:

1. [Serverless indexes](https://docs.pinecone.io/guides/indexes/understanding-indexes#serverless-indexes) (recommended
   for most use cases)
2. [Pod-based indexes](https://docs.pinecone.io/guides/indexes/understanding-indexes#pod-based-indexes) (recommended for
   high-throughput use cases).

#### Create a serverless index

The following is an example of creating a serverless index in the `us-east-1` region of AWS. For more information on
serverless and regional availability,
see [Understanding indexes](https://docs.pinecone.io/guides/indexes/understanding-indexes#serverless-indexes).

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var index = await pinecone.CreateIndexAsync(new CreateIndexRequest
{
   Name = "example-index",
   Dimension = 1538,
   Metric = CreateIndexRequestMetric.Cosine,
   Spec = new ServerlessIndexSpec
   {
       Serverless = new ServerlessSpec
       {
           Cloud = ServerlessSpecCloud.Azure,
           Region = "eastus2",
       }
   },
   DeletionProtection = DeletionProtection.Enabled
});
```

#### Create a pod-based index

The following is a minimal example of creating a pod-based index.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var index = await pinecone.CreateIndexAsync(new CreateIndexRequest
{
   Name = "example-index",
   Dimension = 1538,
   Metric = CreateIndexRequestMetric.Cosine,
   Spec = new PodIndexSpec
   {
       Pod = new PodSpec
       {
           Environment = "eastus-azure",
           PodType = "p1.x1",
           Pods = 1,
           Replicas = 1,
           Shards = 1,
       }
   },
   DeletionProtection = DeletionProtection.Enabled
});
```

### List indexes

The following example returns all indexes (and their corresponding metadata) in your project.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var indexesInYourProject = await pinecone.ListIndexesAsync();
```

### Delete an index

The following example deletes an index by name.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

await pinecone.DeleteIndexAsync("example-index");
```

### Describe an index

The following example returns metadata about an index.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var indexModel = pinecone.DescribeIndexAsync("example-index");
```

### Scale replicas

The following example changes the number of replicas for an index.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var indexMetadata = await pinecone.ConfigureIndexAsync("example-index", new ConfigureIndexRequest
{
   Spec = new ConfigureIndexRequestSpec
   {
       Pod = new ConfigureIndexRequestSpecPod
       {
           Replicas = 2,
           PodType = "p1.x1",
       }
   }
});
```

> Note that scaling replicas is only applicable to pod-based indexes.

### Describe index statistics

The following example returns statistics about an index.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var index = pinecone.Index("example-index");
var indexStatsResponse = await index.DescribeIndexStatsAsync(new DescribeIndexStatsRequest());
```

### Upsert vectors

Operations related to the indexing, deleting, and querying of vectors are called
[data plane](https://docs.pinecone.io/reference/api/introduction#data-plane) operations.

The following example upserts vectors to `example-index`.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var index = pinecone.Index("example-index");

// Vector ids to be upserted
var upsertIds = new[] { "v1", "v2", "v3" };

// List of values to be upserted
float[][] values =
[
    [1.0f, 2.0f, 3.0f],
    [4.0f, 5.0f, 6.0f],
    [7.0f, 8.0f, 9.0f],
];

// List of sparse indices to be upserted
uint[][] sparseIndices =
[
    [1, 2, 3],
    [4, 5, 6],
    [7, 8, 9],
];

// List of sparse values to be upserted
float[][] sparseValues =
[
    [1000f, 2000f, 3000f],
    [4000f, 5000f, 6000f],
    [7000f, 8000f, 9000f],
];

// Metadata to be upserted
var metadataStructArray = new[]
{
    new Metadata { ["genre"] = "action", ["year"] = 2019 },
    new Metadata { ["genre"] = "thriller", ["year"] = 2020 },
    new Metadata { ["genre"] = "comedy", ["year"] = 2021 },
};

var vectors = new List<Vector>();
for (var i = 0; i <= 2; i++)
{
    vectors.Add(
        new Vector
        {
            Id = upsertIds[i],
            Values = values[i],
            SparseValues = new SparseValues
            {
                Indices = sparseIndices[i],
                Values = sparseValues[i],
            },
            Metadata = metadataStructArray[i],
        }
    );
}

var upsertResponse = await index.UpsertAsync(new UpsertRequest { Vectors = vectors, });
```

### Query an index

The following example queries the index `example-index` with metadata filtering.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var index = pinecone.Index("example-index");

var queryResponse = await index.QueryAsync(
   new QueryRequest
   {
       Namespace = "example-namespace",
       Vector = [0.1f, 0.2f, 0.3f, 0.4f],
       TopK = 10,
       IncludeValues = true,
       IncludeMetadata = true,
       Filter = new Metadata
       {
           ["genre"] =
               new Metadata
               {
                   ["$in"] = new[] { "comedy", "documentary", "drama" },
               }
       }
   });
```

### Query sparse-dense vectors

The following example queries an index using a sparse-dense vector:

```csharp
using Pinecone;
var pinecone = new PineconeClient("PINECONE_API_KEY");

var index = pinecone.Index("example-index");

var queryResponse = await index.QueryAsync(
   new QueryRequest
   {
       TopK = 10,
       Vector = [0.1f, 0.2f, 0.3f],
       SparseVector = new SparseValues
       {
           Indices = [10, 45, 16],
           Values = [0.5f, 0.5f, 0.2f],
       }
   }
);
```

### Delete vectors

The following example deletes vectors by ID.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var index = pinecone.Index("example-index");

var deleteResponse = await index.DeleteAsync(new DeleteRequest
{
   Ids = new[] { "v1" },
   Namespace = "example-namespace",
});
```

The following example deletes all records in a namespace and the namespace itself:

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var index = pinecone.Index("example-index");

var deleteResponse = await index.DeleteAsync(new DeleteRequest {
    DeleteAll = true,
    Namespace = "example-namespace",
});
```

### Fetch vectors

The following example fetches vectors by ID.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var index = pinecone.Index("example-index");

var fetchResponse = await index.FetchAsync(new FetchRequest {
    Ids = new[] { "v1" },
    Namespace = "example-namespace",
});
```

### List vector IDs

The following example lists up to 100 vector IDs from a Pinecone index.

The following demonstrates how to use the list endpoint to get vector
IDs from a specific namespace, filtered by a given prefix.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var index = pinecone.Index("example-index");

var listResponse = await index.ListAsync(new ListRequest {
    Namespace = "example-namespace",
    Prefix = "prefix-",
});
```

### Update vectors

The following example updates vectors by ID.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var index = pinecone.Index("example-index");

var updateResponse = await index.UpdateAsync(new UpdateRequest
{
   Id = "vec1",
   Values = new[] { 0.1f, 0.2f, 0.3f, 0.4f },
   SetMetadata = new Metadata { ["genre"] = "drama" },
   Namespace = "example-namespace",
});
```

## Collections

Collections fall under data plane operations.

### Create a collection

The following creates a collection.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var collectionModel = await pinecone.CreateCollectionAsync(new CreateCollectionRequest {
    Name = "example-collection",
    Source = "example-index",
});
```

### List collections

The following example returns a list of the collections in the current project.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var collectionList = await pinecone.ListCollectionsAsync();
```

### Describe a collection

The following example returns a description of the collection.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

var collectionModel = await pinecone.DescribeCollectionAsync("example-collection");
```

### Delete a collection

The following example deletes the collection `example-collection`.

```csharp
using Pinecone;

var pinecone = new PineconeClient("PINECONE_API_KEY");

await pinecone.DeleteCollectionAsync("example-collection");
```

## Advanced

### Control Plane Client Options

Control Plane endpoints are accessed via standard HTTP requests. You can configure the following HTTP client options:

- **MaxRetries**: The maximum number of times the client will retry a failed request. Default is `2`.
- **Timeout**: The time limit for each request before it times out. Default is `30 seconds`.
- **BaseUrl**: The base URL for all requests.
- **HttpClient**: The HTTP client to be used for all requests.

Example usage:

```csharp
var pinecone = new PineconeClient("PINECONE_API_KEY", new ClientOptions
{
    MaxRetries = 3,
    Timeout = TimeSpan.FromSeconds(60),
    HttpClient = ... // Override the Http Client
    BaseUrl = ... // Override the Base URL
});
```

### Data Plane gRPC Options

Data Plane endpoints are accessed via gRPC. You can configure the Pinecone client with gRPC channel options for advanced
control over gRPC communication settings. These options allow you to customize various aspects like message size limits,
retry attempts, credentials, and more.

Example usage:

```csharp
var pinecone = new PineconeClient("PINECONE_API_KEY", new ClientOptions
{
    GrpcOptions = new GrpcChannelOptions
    {
        MaxRetryAttempts = 5,
        MaxReceiveMessageSize = 4 * 1024 * 1024 // 4 MB
        // Additional configuration options...
    }
});
```

### Exception handling

When the API returns a non-zero status code, (4xx or 5xx response), a subclass of
`PineconeException` will be thrown:

```csharp
try {
    pinecone.CreateIndexAsync(...);
} catch (PineconeException e) {
    System.Console.WriteLine(e.Message)
    System.Console.WriteLine(e.StatusCode)
}
```

## Contributing

While we value open-source contributions to this SDK, this library
is generated programmatically. Additions made directly to this library
would have to be moved over to our generation code, otherwise they would
be overwritten upon the next generated release. Feel free to open a PR as a
proof of concept, but know that we will not be able to Pinecone it as-is.
We suggest opening an issue first to discuss with us!

On the other hand, contributions to the README are always very welcome!
