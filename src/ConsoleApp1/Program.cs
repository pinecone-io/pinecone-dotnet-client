using Pinecone.Client;
using System.Text.Json;

// Pinecone official
var pinecone = new Pinecone.Client.Pinecone("5e1c8d8d-9e0c-4d90-b29d-4b2929a830a2");

// // Pinecone field eng
// var pinecone = new Pinecone.Client.Pinecone("8da23992-6bc0-480a-9f9e-1acfb3d2dfbf");

// // Create serverless index
// var createIndexRequest = new CreateIndexRequest
// {
//     Name = "multitenant-app",
//     Dimension = 8,
//     Metric = CreateIndexRequestMetric.Cosine,
//     Spec = new ServerlessIndexSpec
//     {
//         Serverless = new ServerlessSpec
//         {
//             Cloud = ServerlessSpecCloud.Aws,
//             Region = "us-east-1"
//         }
//     },
//     DeletionProtection = DeletionProtection.Disabled
// };

// var index = await pinecone.CreateIndexAsync(createIndexRequest);

// // Create pod index
// var createIndexRequest = new CreateIndexRequest
// {
//     Name = "csharppod",
//     Dimension = 1538,
//     Metric = CreateIndexRequestMetric.Cosine,
//     Spec = new PodIndexSpec
//     {
//         Pod = new PodSpec
//         {
//             Environment = "us-east-1-aws",
//             PodType = "p1.x1",
//             Pods = 1,
//             Replicas = 1,
//             Shards = 1,
            // MetadataConfig = new PodSpecMetadataConfig
            // {
            //     Indexed = new List<string> { "id" }
            // }
//         }
//     },
//     DeletionProtection = DeletionProtection.Enabled,
// };

// var index = await pinecone.CreateIndexAsync(createIndexRequest);

// // Create pod index from collection
// var createIndexRequest = new CreateIndexRequest
// {
//     Name = "csharppod",
//     Dimension = 1538,
//     Metric = CreateIndexRequestMetric.Cosine,
//     Spec = new PodIndexSpec
//     {
//         Pod = new PodSpec
//         {
//             Environment = "us-east-1-aws",
//             PodType = "p1.x1",
//             Pods = 1,
//             Replicas = 1,
//             Shards = 1,
//         }
//     },
//     DeletionProtection = DeletionProtection.Enabled,
//     Collection = "csharppod",
// };

// var index = await pinecone.CreateIndexAsync(createIndexRequest);

// // List indexes
// var indexesInYourProject = await pinecone.ListIndexesAsync();

// var options = new JsonSerializerOptions { WriteIndented = true };
// string jsonString = JsonSerializer.Serialize(indexesInYourProject, options);
// Console.Write(jsonString);

// // Delete an index
// await pinecone.DeleteIndexAsync("csharp1");

// // Describe an index
// var indexModel = await pinecone.DescribeIndexAsync("csharppod");

// var options = new JsonSerializerOptions { WriteIndented = true };
// string jsonString = JsonSerializer.Serialize(indexModel, options);
// Console.Write(jsonString);

// Disable deletion protection
// var configureIndexRequest = new ConfigureIndexRequest
// {
//     DeletionProtection = DeletionProtection.Disabled,
// };

// var indexMetadata = await pinecone.ConfigureIndexAsync("youtube", configureIndexRequest);

// // Scale replicas
// var configureIndexRequest = new ConfigureIndexRequest
// {
//     Spec = new ConfigureIndexRequestSpec
//     {
//         Pod = new ConfigureIndexRequestSpecPod {
//             Replicas = 2,
//             PodType = "p1.x1"
//         }
//     }
// };
//
// var indexMetadata = await pinecone.ConfigureIndexAsync("csharppod", configureIndexRequest);

// // Upsert vectors
// var index = pinecone.Index("docs-quickstart-index");

// await index.UpsertAsync(new UpsertRequest {
//     Vectors = new[]
//     {
//         new Vector
//         {
//             Id = "vec22",
//             Values = new[] { 1.0f, 1.5f },
//             SparseValues = new SparseValues
//             {
//                 Indices = [0, 1],
//                 Values = new[] { 1.0f, 1.5f }
//             },
//             Metadata = new Dictionary<string, MetadataValue?> {
//                 ["genre"] = new("horror"),
//                 ["year"] = new(2020),
//             }
//         }
//     }
// });



// // Upsert vectors
// var index = pinecone.Index("docs-quickstart-index");

// var upsertResponse1 = await index.UpsertAsync(new UpsertRequest {
//     Vectors = new[]
//     {
//         new Vector
//         {
//             Id = "vec1",
//             Values = new[] { 1.0f, 1.5f },
//         },
//         new Vector
//         {
//             Id = "vec2",
//             Values = new[] { 2.0f, 1.0f },
//         },
//         new Vector
//         {
//             Id = "vec3",
//             Values = new[] { 0.1f, 3.0f },
//         }
//     },
//     Namespace = "ns1"
// });

// var upsertResponse2 = await index.UpsertAsync(new UpsertRequest {
//     Vectors = new[]
//     {
//         new Vector
//         {
//             Id = "vec1",
//             Values = new[] { 1.0f, -2.5f },
//         },
//         new Vector
//         {
//             Id = "vec2",
//             Values = new[] { 3.0f, -2.0f },
//         },
//         new Vector
//         {
//             Id = "vec3",
//             Values = new[] { 0.5f, -1.5f },
//         }
//     },
//     Namespace = "ns2"
// });

// // Describe index stats
// var index = pinecone.Index("docs-quickstart-index");

// var indexStatsResponse = await index.DescribeIndexStatsAsync(new DescribeIndexStatsRequest());

// var options = new JsonSerializerOptions { WriteIndented = true };
// string jsonString = JsonSerializer.Serialize(indexStatsResponse, options);

// Console.Write(jsonString);

// Query with metadata filter (not working)
var index = pinecone.Index("docs-quickstart-index");

var queryResponse = await index.QueryAsync(new QueryRequest {
    Vector = new[] { 1.0f, 1.5f },
    Namespace = "",
    TopK = 3,
    Filter = new Dictionary<string, MetadataValue?>
    {
        ["genre"] =
            new Dictionary<string, MetadataValue?>
            {
                ["$in"] = new[] { "comedy", "documentary", "drama" }
            }
    }
});

// var options = new JsonSerializerOptions { WriteIndented = true };
// string jsonString = JsonSerializer.Serialize(queryResponse, options);

// Console.Write(jsonString);

// // Query
// var index = pinecone.Index("docs-quickstart-index");

// var queryResponse1 = await index.QueryAsync(new QueryRequest {
//     Vector = new[] { 1.0f, 1.5f },
//     Namespace = "ns1",
//     TopK = 3,
// });

// var queryResponse2 = await index.QueryAsync(new QueryRequest {
//     Vector = new[] { 1.0f, -2.5f },
//     Namespace = "ns2",
//     TopK = 3,
// });


// var options = new JsonSerializerOptions { WriteIndented = true };
// string jsonString1 = JsonSerializer.Serialize(queryResponse1, options);
// string jsonString2 = JsonSerializer.Serialize(queryResponse2, options);

// Console.Write(jsonString1);
// Console.Write(jsonString2);

// // Delete vectors
//
// var index = pinecone.Index("docs-quickstart-index");
//
// var deleteResponse = await index.DeleteAsync(new DeleteRequest {
//     Ids = new List<string> { "vec1", "vec2" },
//     Namespace = "ns2",
// });

// // // Delete namespace (and all vectors)
// var index = pinecone.Index("docs-quickstart-index");

// var deleteResponse = await index.DeleteAsync(new DeleteRequest {
//     DeleteAll = true,
//     Namespace = "ns1",
// });

// // Fetch vectors
// var index = pinecone.Index("listids");

// var fetchResponse = await index.FetchAsync(new FetchRequest {
//     Ids = new List<string> { "doc1#chunk1" },
//     Namespace = "ns1",
// });

// var options = new JsonSerializerOptions { WriteIndented = true };
// string jsonString = JsonSerializer.Serialize(fetchResponse, options);
// Console.Write(jsonString);

// // List IDs
// var index = pinecone.Index("listids");
//
// var listResponse = await index.ListAsync(new ListRequest {
//     Namespace = "ns1",
//     Prefix = "doc1#",
// });
//
// var options = new JsonSerializerOptions { WriteIndented = true };
// string jsonString = JsonSerializer.Serialize(listResponse, options);
// Console.Write(jsonString);

// // Update vectors
// var index = pinecone.Index("listids");

// var updateResponse = await index.UpdateAsync(new UpdateRequest {
//     Id = "vec1",
//     Namespace = "ns1",
//     Values = new[] { 2.555f, 5.555f },    
//     SetMetadata = new Dictionary<string, MetadataValue?> {
//         ["genre"] = new("comedy"),
//     }
// });


// // Create a collection
// var collectionModel = await pinecone.CreateCollectionAsync(new CreateCollectionRequest {
//     Name = "collection-csharppod",
//     Source = "csharppod",
// });

// // List collections
// var collectionList = await pinecone.ListCollectionsAsync();
//
// var options = new JsonSerializerOptions { WriteIndented = true };
// string jsonString = JsonSerializer.Serialize(collectionList, options);
// Console.Write(jsonString);

// // Describe a collection
// var collectionModel = await pinecone.DescribeCollectionAsync("collection-csharppod");
//
// var options = new JsonSerializerOptions { WriteIndented = true };
// string jsonString = JsonSerializer.Serialize(collectionModel, options);
// Console.Write(jsonString);

// // Delete a collection
// await pinecone.DeleteCollectionAsync("collection-csharppod");

// // Delete index
// await pinecone.DeleteIndexAsync("docs-quickstart-index");
