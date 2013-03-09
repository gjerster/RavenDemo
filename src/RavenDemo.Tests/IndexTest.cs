using System.Collections.Generic;
using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using Raven.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace RavenDemo.Tests
{
    public class IndexTest : RavenTestBase
    {
        [Theory]
        [InlineData("singa*")]
        [InlineData("pte")]
        [InlineData("ltd")]
        public void CanSearchWithPrefixWildcard(string query)
        {
            using (EmbeddableDocumentStore store = NewDocumentStore())
            {
                IndexCreation.CreateIndexes(typeof (SampleDataIndex).Assembly, store);
                CreateSampleData(store);

                using (IDocumentSession session = store.OpenSession())
                {
                    IRavenQueryable<SampleDataIndex.ReducedResult> rq = session
                        .Query<SampleDataIndex.ReducedResult, SampleDataIndex>()
                        .Customize(customization => customization.WaitForNonStaleResultsAsOfNow());
                    List<SampleData> result =
                        rq.Search(x => x.Query, query,escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard)
                          .As<SampleData>()
                          .Take(10)
                          .ToList();
                    Assert.NotEmpty(result);
                }
            }
        }

        private void CreateSampleData(EmbeddableDocumentStore store)
        {
            using (IDocumentSession session = store.OpenSession())
            {
                session.Store(new SampleData
                {
                    Name = "Singapore",
                    Description = "SINGAPORE PTE LTD"
                });

                session.SaveChanges();
            }
        }
    }

    public class SampleData
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SampleDataIndex : AbstractIndexCreationTask<SampleData, SampleDataIndex.ReducedResult>
    {
        public SampleDataIndex()
        {
            Map = docs => from doc in docs
                          select new
                              {
                                  Query = new object[]
                                      {
                                          doc.Name,
                                          doc.Description
                                      }
                              };
            Indexes.Add(x => x.Query, FieldIndexing.Analyzed);
        }

        public class ReducedResult
        {
            public string Query { get; set; }
        }
    }
}