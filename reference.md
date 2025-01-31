# Reference
<details><summary><code>client.<a href="/src/Pinecone/BasePinecone.cs">ListIndexesAsync</a>() -> IndexList</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

This operation returns a list of all indexes in a project.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ListIndexesAsync();
```
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.<a href="/src/Pinecone/BasePinecone.cs">CreateIndexAsync</a>(CreateIndexRequest { ... }) -> Index</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

This operation deploys a Pinecone index. This is where you specify the measure of similarity, the dimension of vectors to be stored in the index, which cloud provider you would like to deploy with, and more.
  
For guidance and examples, see [Create an index](https://docs.pinecone.io/guides/indexes/create-an-index#create-a-serverless-index).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.CreateIndexAsync(
    new CreateIndexRequest
    {
        Name = "movie-recommendations",
        Dimension = 1536,
        Metric = CreateIndexRequestMetric.Cosine,
        DeletionProtection = DeletionProtection.Enabled,
        Spec = new ServerlessIndexSpec
        {
            Serverless = new ServerlessSpec
            {
                Cloud = ServerlessSpecCloud.Gcp,
                Region = "us-east1",
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateIndexRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.<a href="/src/Pinecone/BasePinecone.cs">DescribeIndexAsync</a>(indexName) -> Index</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Get a description of an index.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.DescribeIndexAsync("test-index");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**indexName:** `string` — The name of the index to be described.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.<a href="/src/Pinecone/BasePinecone.cs">DeleteIndexAsync</a>(indexName)</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

This operation deletes an existing index.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.DeleteIndexAsync("test-index");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**indexName:** `string` — The name of the index to delete.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.<a href="/src/Pinecone/BasePinecone.cs">ConfigureIndexAsync</a>(indexName, ConfigureIndexRequest { ... }) -> Index</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

This operation configures an existing index. 

For serverless indexes, you can configure index deletion protection, tags, and integrated inference embedding settings for the index. For pod-based indexes, you can configure the pod size, number of replicas, tags, and index deletion protection.

It is not possible to change the pod type of a pod-based index. However, you can create a collection from a pod-based index and then [create a new pod-based index with a different pod type](http://docs.pinecone.io/guides/indexes/create-an-index#create-an-index-from-a-collection) from the collection. For guidance and examples, see [Configure an index](http://docs.pinecone.io/guides/indexes/configure-an-index).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ConfigureIndexAsync(
    "test-index",
    new ConfigureIndexRequest
    {
        Spec = new ConfigureIndexRequestSpec
        {
            Pod = new ConfigureIndexRequestSpecPod { PodType = "p1.x2" },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**indexName:** `string` — The name of the index to configure.
    
</dd>
</dl>

<dl>
<dd>

**request:** `ConfigureIndexRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.<a href="/src/Pinecone/BasePinecone.cs">ListCollectionsAsync</a>() -> CollectionList</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

This operation returns a list of all collections in a project.
Serverless indexes do not support collections.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ListCollectionsAsync();
```
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.<a href="/src/Pinecone/BasePinecone.cs">CreateCollectionAsync</a>(CreateCollectionRequest { ... }) -> CollectionModel</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

This operation creates a Pinecone collection.
  
Serverless indexes do not support collections.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.CreateCollectionAsync(
    new CreateCollectionRequest { Name = "example-collection", Source = "example-source-index" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateCollectionRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.<a href="/src/Pinecone/BasePinecone.cs">CreateIndexForModelAsync</a>(CreateIndexForModelRequest { ... }) -> Index</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

This operation creates a serverless integrated inference index for a specific embedding model.

Refer to the [model guide](https://docs.pinecone.io/guides/inference/understanding-inference#embedding-models) for available models and model details.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.CreateIndexForModelAsync(
    new CreateIndexForModelRequest
    {
        Name = "multilingual-e5-large-index",
        Cloud = CreateIndexForModelRequestCloud.Gcp,
        Region = "us-east1",
        DeletionProtection = DeletionProtection.Enabled,
        Embed = new CreateIndexForModelRequestEmbed
        {
            Model = "multilingual-e5-large",
            Metric = CreateIndexForModelRequestEmbedMetric.Cosine,
            FieldMap = new Dictionary<string, object>() { { "text", "your-text-field" } },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateIndexForModelRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.<a href="/src/Pinecone/BasePinecone.cs">DescribeCollectionAsync</a>(collectionName) -> CollectionModel</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

This operation gets a description of a collection.
Serverless indexes do not support collections.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.DescribeCollectionAsync("tiny-collection");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**collectionName:** `string` — The name of the collection to be described.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.<a href="/src/Pinecone/BasePinecone.cs">DeleteCollectionAsync</a>(collectionName)</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

This operation deletes an existing collection.
Serverless indexes do not support collections.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.DeleteCollectionAsync("test-collection");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**collectionName:** `string` — The name of the collection.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Index
<details><summary><code>client.Index.<a href="/src/Pinecone/Index/IndexClient.cs">ListBulkImportsAsync</a>(ListBulkImportsRequest { ... }) -> ListImportsResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

The `list_imports` operation lists all recent and ongoing import operations.

By default, `list_imports` returns up to 100 imports per page. If the `limit` parameter is set, `list` returns up to that number of imports instead. Whenever there are additional IDs to return, the response also includes a `pagination_token` that you can use to get the next batch of imports. When the response does not include a `pagination_token`, there are no more imports to return.

For guidance and examples, see [Import data](https://docs.pinecone.io/guides/data/import-data).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Index.ListBulkImportsAsync(new ListBulkImportsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListBulkImportsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Index.<a href="/src/Pinecone/Index/IndexClient.cs">StartBulkImportAsync</a>(StartImportRequest { ... }) -> StartImportResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

The `start_import` operation starts an asynchronous import of vectors from object storage into an index.

For guidance and examples, see [Import data](https://docs.pinecone.io/guides/data/import-data).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Index.StartBulkImportAsync(new StartImportRequest { Uri = "uri" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `StartImportRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Index.<a href="/src/Pinecone/Index/IndexClient.cs">DescribeBulkImportAsync</a>(id) -> ImportModel</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

The `describe_import` operation returns details of a specific import operation.

For guidance and examples, see [Import data](https://docs.pinecone.io/guides/data/import-data).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Index.DescribeBulkImportAsync("101");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Unique identifier for the import operation.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Index.<a href="/src/Pinecone/Index/IndexClient.cs">CancelBulkImportAsync</a>(id) -> CancelImportResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

The `cancel_import` operation cancels an import operation if it is not yet finished. It has no effect if the operation is already finished.

For guidance and examples, see [Import data](https://docs.pinecone.io/guides/data/import-data).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Index.CancelBulkImportAsync("101");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — Unique identifier for the import operation.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Index.<a href="/src/Pinecone/Index/IndexClient.cs">DescribeIndexStatsAsync</a>(DescribeIndexStatsRequest { ... }) -> DescribeIndexStatsResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Get index stats

 The `describe_index_stats` operation returns statistics about the contents of an index, including the vector count per namespace, the number of dimensions, and the index fullness.

 Serverless indexes scale automatically as needed, so index fullness is relevant only for pod-based indexes.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Index.DescribeIndexStatsAsync(new DescribeIndexStatsRequest());
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DescribeIndexStatsRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Index.<a href="/src/Pinecone/Index/IndexClient.cs">QueryAsync</a>(QueryRequest { ... }) -> QueryResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Query vectors

 The `query` operation searches a namespace, using a query vector. It retrieves the ids of the most similar items in a namespace, along with their similarity scores.

 For guidance and examples, see [Query data](https://docs.pinecone.io/guides/data/query-data).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Index.QueryAsync(
    new QueryRequest
    {
        TopK = 3,
        Namespace = "example",
        IncludeValues = true,
        IncludeMetadata = true,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `QueryRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Index.<a href="/src/Pinecone/Index/IndexClient.cs">DeleteAsync</a>(DeleteRequest { ... }) -> DeleteResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Delete vectors

 The `delete` operation deletes vectors, by id, from a single namespace.

 For guidance and examples, see [Delete data](https://docs.pinecone.io/guides/data/delete-data).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Index.DeleteAsync(
    new DeleteRequest
    {
        Ids = new List<string>() { "v1", "v2", "v3" },
        Namespace = "example",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `DeleteRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Index.<a href="/src/Pinecone/Index/IndexClient.cs">FetchAsync</a>(FetchRequest { ... }) -> FetchResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Fetch vectors

 The `fetch` operation looks up and returns vectors, by ID, from a single namespace. The returned vectors include the vector data and/or metadata.

 For guidance and examples, see [Fetch data](https://docs.pinecone.io/guides/data/fetch-data).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Index.FetchAsync(new FetchRequest { Ids = ["v1"], Namespace = "example" });
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `FetchRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Index.<a href="/src/Pinecone/Index/IndexClient.cs">ListAsync</a>(ListRequest { ... }) -> ListResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

List vector IDs

 The `list` operation lists the IDs of vectors in a single namespace of a serverless index. An optional prefix can be passed to limit the results to IDs with a common prefix.

 `list` returns up to 100 IDs at a time by default in sorted order (bitwise/"C" collation). If the `limit` parameter is set, `list` returns up to that number of IDs instead. Whenever there are additional IDs to return, the response also includes a `pagination_token` that you can use to get the next batch of IDs. When the response does not include a `pagination_token`, there are no more IDs to return.

 For guidance and examples, see [List record IDs](https://docs.pinecone.io/guides/data/list-record-ids).

 **Note:** `list` is supported only for serverless indexes.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Index.ListAsync(
    new ListRequest
    {
        Limit = 50,
        Namespace = "example",
        PaginationToken = "eyJza2lwX3Bhc3QiOiIxMDEwMy0=",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Index.<a href="/src/Pinecone/Index/IndexClient.cs">UpdateAsync</a>(UpdateRequest { ... }) -> UpdateResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Update a vector

 The `update` operation updates a vector in a namespace. If a value is included, it will overwrite the previous value. If a `set_metadata` is included, the values of the fields specified in it will be added or overwrite the previous value.

 For guidance and examples, see [Update data](https://docs.pinecone.io/guides/data/update-data).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Index.UpdateAsync(
    new UpdateRequest
    {
        Id = "v1",
        Namespace = "example",
        Values = new[] { 42.2f, 50.5f, 60.8f },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpdateRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Index.<a href="/src/Pinecone/Index/IndexClient.cs">UpsertAsync</a>(UpsertRequest { ... }) -> UpsertResponse</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Upsert vectors

 The `upsert` operation writes vectors into a namespace. If a new value is upserted for an existing vector ID, it will overwrite the previous value.

 For guidance and examples, see [Upsert data](https://docs.pinecone.io/guides/data/upsert-data).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Index.UpsertAsync(
    new UpsertRequest
    {
        Vectors = new List<Vector>()
        {
            new Vector { Id = "v1", Values = new[] { 0.1f, 0.2f, 0.3f } },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `UpsertRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## Inference
<details><summary><code>client.Inference.<a href="/src/Pinecone/Inference/InferenceClient.cs">EmbedAsync</a>(EmbedRequest { ... }) -> EmbeddingsList</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Generate embeddings for input data.

For guidance and examples, see [Generate embeddings](https://docs.pinecone.io/guides/inference/generate-embeddings).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Inference.EmbedAsync(
    new EmbedRequest
    {
        Model = "multilingual-e5-large",
        Inputs = new List<EmbedRequestInputsItem>() { new EmbedRequestInputsItem() },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `EmbedRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.Inference.<a href="/src/Pinecone/Inference/InferenceClient.cs">RerankAsync</a>(RerankRequest { ... }) -> RerankResult</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Rerank documents according to their relevance to a query.

For guidance and examples, see [Rerank documents](https://docs.pinecone.io/guides/inference/rerank).
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Inference.RerankAsync(
    new RerankRequest
    {
        Model = "bge-reranker-v2-m3",
        Query = "What is the capital of France?",
        Documents = new List<Dictionary<string, object?>>()
        {
            new Dictionary<string, object>()
            {
                { "id", "1" },
                { "text", "Paris is the capital of France." },
                { "title", "France" },
                { "url", "https://example.com" },
            },
        },
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `RerankRequest` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>
