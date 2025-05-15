using System.Net;
using NUnit.Framework;

namespace Pinecone.Test.Integration.Data;

[TestFixture]
[Explicit("This test should be run manually")]
public class TestBackups : BaseTest
{
    private BackupModel _backup;
    private string _indexName;
    private string _restoreBackupJobId;

    private readonly RequestOptions _options = new()
    {
        // HttpClient = new HttpClient(new HttpClientHandler
        // {
        //     Proxy = new WebProxy("127.0.0.1:9090")
        // })
    };

    [OneTimeSetUp]
    public async Task SetUp()
    {
        await Task.Delay(TimeSpan.FromSeconds(20));
        _backup = await Client.Backups.BackupIndexAsync(IndexName, new BackupIndexRequest(), _options);
        await Task.Delay(TimeSpan.FromSeconds(20));
        _indexName = Helpers.GenerateIndexName("test-restore-job-setup");
        var response = await Client.Backups.CreateIndexFromBackupAsync(_backup.BackupId,
            new CreateIndexFromBackupRequest
            {
                Name = _indexName,
                DeletionProtection = DeletionProtection.Disabled,
            }, _options);
        _restoreBackupJobId = response.RestoreJobId;
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        try
        {
            await Client.Backups.DeleteAsync(_backup.BackupId, _options);
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            await Client.DeleteIndexAsync(_indexName, _options);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    [Test]
    public async Task TestListBackups()
    {
        var backups = await Client.Backups.ListAsync(_options);
        Assert.That(backups, Is.Not.Null);
        Assert.That(backups.Data, Is.Not.Null);
        Assert.That(backups.Data, Is.Not.Empty);
    }

    [Test]
    public async Task TestListBackupsByIndex()
    {
        var backups = await Client.Backups.ListByIndexAsync(
            IndexName, 
            new ListBackupsByIndexRequest(), 
            _options
        );
        Assert.That(backups, Is.Not.Null);
        Assert.That(backups.Data, Is.Not.Null);
        Assert.That(backups.Data, Is.Not.Empty);
    }

    [Test]
    public async Task TestGetBackup()
    {
        var backup = await Client.Backups.GetAsync(_backup.BackupId, _options);
        Assert.That(backup, Is.Not.Null);
        Assert.That(backup.Name, Is.EqualTo(_backup.Name));
    }

    [Test]
    public async Task TestDeleteBackup()
    {
        await Task.Delay(TimeSpan.FromSeconds(20));
        var backup = await Client.Backups.BackupIndexAsync(IndexName, new BackupIndexRequest(), _options);
        await Task.Delay(TimeSpan.FromSeconds(20));
        await Client.Backups.DeleteAsync(backup.BackupId, _options);
    }

    [Test]
    public async Task TestCreateIndexFromBackup()
    {
        var indexName = Helpers.GenerateIndexName("test-create-index-from-backup");
        await Client.Backups.CreateIndexFromBackupAsync(_backup.BackupId, new CreateIndexFromBackupRequest
        {
            Name = indexName,
            DeletionProtection = DeletionProtection.Disabled,
        }, _options);
        await Client.DeleteIndexAsync(indexName, _options);
    }

    [Test]
    public async Task TestListRestoreJobs()
    {
        var restoreJobs = await Client.RestoreJobs.ListAsync(new ListRestoreJobsRequest(), _options);
        Assert.That(restoreJobs, Is.Not.Null);
        Assert.That(restoreJobs.Data, Is.Not.Null);
        Assert.That(restoreJobs.Data, Is.Not.Empty);
    }

    [Test]
    public async Task TestGetRestoreJobs()
    {
        var restoreJob = await Client.RestoreJobs.GetAsync(_restoreBackupJobId, _options);
        Assert.That(restoreJob, Is.Not.Null);
        Assert.That(restoreJob.TargetIndexName, Is.EqualTo(_indexName));
    }
}