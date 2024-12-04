using System.Text.Json;
using System.Text.Json.Nodes;
using NUnit.Framework;
using Pinecone.Core;

namespace Pinecone.Test.Integration.Data;

public class TestBulkOperations : BaseTest
{
    private const string ImportUri = "s3://BUCKET_NAME/PATH/TO/DIR";

    [Test]
    public async Task TestStartBulkImport()
    {
        var response = await IndexClient.StartBulkImportAsync(
            new StartImportRequest
            {
                Uri = ImportUri,
                ErrorMode = new ImportErrorMode
                {
                    OnError = ImportErrorModeOnError.Abort
                }
            }
        );

        Assert.That(response, Is.InstanceOf<StartImportResponse>());
        Assert.That(response.Id, Is.Not.Null);
    }

    [Test]
    public async Task TestCancelBulkImport()
    {
        var startResponse = await IndexClient.StartBulkImportAsync(
            new StartImportRequest
            {
                Uri = ImportUri,
                ErrorMode = new ImportErrorMode
                {
                    OnError = ImportErrorModeOnError.Abort
                }
            }
        );
        Assert.That(startResponse.Id, Is.Not.Null);

        var cancelResponse = await IndexClient.CancelBulkImportAsync(startResponse.Id);
        Assert.That(cancelResponse, Is.InstanceOf<object>());
    }

    [Test]
    public async Task TestDescribeBulkImport()
    {
        var startResponse = await IndexClient.StartBulkImportAsync(
            new StartImportRequest
            {
                Uri = ImportUri,
                ErrorMode = new ImportErrorMode
                {
                    OnError = ImportErrorModeOnError.Abort
                }
            }
        );

        Assert.That(startResponse, Is.InstanceOf<StartImportResponse>());
        Assert.That(startResponse.Id, Is.Not.Null);

        // Describe the bulk import
        var describeResponse = await IndexClient.DescribeBulkImportAsync(startResponse.Id);
        Assert.That(describeResponse, Is.InstanceOf<ImportModel>());
        Assert.Multiple(() =>
        {
            Assert.That(describeResponse.Id, Is.EqualTo(startResponse.Id));
            Assert.That(describeResponse.Status, Is.Not.Null);
            Assert.That(describeResponse.Uri, Is.EqualTo(ImportUri));
            Assert.That(describeResponse.CreatedAt, Is.Not.Null);
        });
    }

    [Test]
    public async Task TestListBulkImport()
    {
        for (var i = 0; i < 5; i++)
        {
            await IndexClient.StartBulkImportAsync(
                new StartImportRequest
                {
                    Uri = ImportUri,
                    ErrorMode = new ImportErrorMode
                    {
                        OnError = ImportErrorModeOnError.Abort
                    }
                }
            );
        }

        var page = 0;
        string? paginationToken = null;
        do
        {
            var listResponse = await IndexClient.ListBulkImportsAsync(new ListBulkImportsRequest
            {
                Limit = 1,
                PaginationToken = paginationToken
            });
            paginationToken = listResponse.Pagination?.Next;
            Assert.That(listResponse.Data, Is.Not.Null);

            foreach (var importModel in listResponse.Data)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(importModel.Id, Is.Not.Null);
                    Assert.That(importModel.Status, Is.Not.Null);
                    Assert.That(importModel.Uri, Is.EqualTo(ImportUri));
                    Assert.That(importModel.CreatedAt, Is.Not.Null);
                });
            }

            page++;
        } while (paginationToken != null);

        Assert.That(page, Is.GreaterThan(0));
    }

    [Test]
    public void TestCreateBulkImportError()
    {
        var exception = Assert.ThrowsAsync<PineconeApiException>(async () => await IndexClient.StartBulkImportAsync(
            new StartImportRequest
            {
                Uri = null!
            }));

        TestContext.Out.WriteLine("Exception body: " + exception.Body);

        Assert.That(exception, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(exception.StatusCode, Is.EqualTo(400));
            Assert.That(exception.Body, Is.InstanceOf<string>());
        });
        var errorResponse = JsonUtils.Deserialize<JsonObject>((string)exception.Body);
        Assert.That(errorResponse, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(errorResponse["code"]?.GetValue<int>(), Is.EqualTo(3));
            Assert.That(errorResponse["message"]?.GetValue<string>(), Is.EqualTo("Import URI parameter is required. Import URI must contain between 1 and 1500 characters."));
        });
    }
}