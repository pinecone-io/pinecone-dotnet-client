using NUnit.Framework;

namespace Pinecone.Test.Integration;

public class TestSetupLists : BaseDataPlaneTest
{
    [Test]
    public async Task TestListWithDefaults()
    {
        var pageSizes = new List<int>();
        var pageCount = 0;

        string? paginationToken = null;
        do
        {
            var listRequest = new ListRequest { PaginationToken = paginationToken };
            var listResult = await _indexClient.ListAsync(listRequest);

            Assert.That(listResult.Vectors, Is.Not.Null);
            paginationToken = listResult.Pagination?.Next;
            if (listResult.Vectors?.Count() > 0)
            {
                pageCount++;
            }
            pageSizes.Add(listResult.Vectors.Count());
        } while (paginationToken != null);

        Assert.That(pageCount, Is.EqualTo(1));
        Assert.That(pageSizes, Is.EqualTo(new List<int> { 9 }));
    }

    [Test]
    public async Task TestList()
    {
        var pageCount = 0;

        var listRequest = new ListRequest
        {
            Prefix = "99",
            Limit = 20,
            Namespace = _listNamespace,
        };
        var listResult = await _indexClient.ListAsync(listRequest);

        Assert.That(listResult.Vectors, Is.Not.Null);
        pageCount++;

        Assert.That(listResult.Vectors.Count(), Is.EqualTo(11));
        Assert.That(
            listResult.Vectors.Select(vector => vector.Id),
            Is.EquivalentTo(
                new List<string>
                {
                    "99",
                    "990",
                    "991",
                    "992",
                    "993",
                    "994",
                    "995",
                    "996",
                    "997",
                    "998",
                    "999"
                }
            )
        );

        Assert.That(pageCount, Is.EqualTo(1));
    }

    [Test]
    public async Task TestListWhenNoResultsForPrefix()
    {
        var pageCount = 0;

        string? paginationToken = null;
        do
        {
            var listRequest = new ListRequest
            {
                Prefix = "no-results",
                Namespace = _listNamespace,
                PaginationToken = paginationToken
            };
            var listResult = await _indexClient.ListAsync(listRequest);

            paginationToken = listResult.Pagination?.Next;
            if (listResult.Vectors?.Count() > 0)
            {
                pageCount++;
            }
        } while (paginationToken != null);

        Assert.That(pageCount, Is.EqualTo(0));
    }

    [Test]
    public async Task TestListWhenNoResultsForNamespace()
    {
        var pageCount = 0;

        string? paginationToken = null;
        do
        {
            var listRequest = new ListRequest
            {
                Prefix = "99",
                Namespace = "no-results",
                PaginationToken = paginationToken
            };
            var listResult = await _indexClient.ListAsync(listRequest);

            paginationToken = listResult.Pagination?.Next;
            if (listResult.Vectors?.Count() > 0)
            {
                pageCount++;
            }
        } while (paginationToken != null);

        Assert.That(pageCount, Is.EqualTo(0));
    }

    [Test]
    public async Task TestListWhenMultiplePages()
    {
        var pages = new List<IEnumerable<string>>();
        var pageSizes = new List<int>();
        var pageCount = 0;

        string? paginationToken = null;
        do
        {
            var listRequest = new ListRequest
            {
                Prefix = "99",
                Limit = 5,
                Namespace = _listNamespace,
                PaginationToken = paginationToken
            };
            var listResult = await _indexClient.ListAsync(listRequest);

            Assert.That(listResult.Vectors, Is.Not.Null);
            paginationToken = listResult.Pagination?.Next;
            if (!(listResult.Vectors?.Count() > 0))
                continue;
            pageCount++;
            pageSizes.Add(listResult.Vectors.Count());
            pages.Add(listResult.Vectors.Select(vector => vector.Id)!);
        } while (paginationToken != null);

        Assert.That(pageCount, Is.EqualTo(3));
        Assert.That(pageSizes, Is.EqualTo(new List<int> { 5, 5, 1 }));
        Assert.That(
            pages[0],
            Is.EquivalentTo(new List<string> { "99", "990", "991", "992", "993" })
        );
        Assert.That(
            pages[1],
            Is.EquivalentTo(new List<string> { "994", "995", "996", "997", "998" })
        );
        Assert.That(pages[2], Is.EquivalentTo(new List<string> { "999" }));
    }

    [Test]
    public async Task TestListThenFetch()
    {
        var vectors = new List<Vector>();

        string? paginationToken = null;
        do
        {
            var listRequest = new ListRequest
            {
                Prefix = "99",
                Limit = 5,
                Namespace = _listNamespace,
                PaginationToken = paginationToken
            };
            var listResult = await _indexClient.ListAsync(listRequest);
            if (listResult.Vectors?.Count() > 0)
            {
                var result = await _indexClient.FetchAsync(
                    new FetchRequest
                    {
                        Ids = listResult.Vectors.Select(vector => vector.Id!),
                        Namespace = _listNamespace
                    }
                );
                vectors.AddRange(result.Vectors!.Values);
            }

            paginationToken = listResult.Pagination?.Next;
        } while (paginationToken != null);

        Assert.That(vectors.Count, Is.EqualTo(11));
        Assert.That(vectors[0], Is.InstanceOf<Vector>());
        Assert.That(
            vectors.Select(v => v.Id).ToHashSet(),
            Is.EquivalentTo(
                new HashSet<string>
                {
                    "99",
                    "990",
                    "991",
                    "992",
                    "993",
                    "994",
                    "995",
                    "996",
                    "997",
                    "998",
                    "999"
                }
            )
        );
    }
}
