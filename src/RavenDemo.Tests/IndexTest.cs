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
                IndexCreation.CreateIndexes(typeof (CustomerSearchIndex).Assembly, store);
                CreateSampleData(store);

                using (IDocumentSession session = store.OpenSession())
                {
                    IRavenQueryable<CustomerSearchIndex.ReducedResult> rq = session
                        .Query<CustomerSearchIndex.ReducedResult, CustomerSearchIndex>()
                        .Customize(customization => customization.WaitForNonStaleResultsAsOfNow());
                    List<Customer> result =
                        rq.Search(x => x.Query, query,escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard)
                          .As<Customer>()
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
                session.Store(new Customer
                {
                    Name = "Singapore",
                    Description = "SINGAPORE PTE LTD"
                });

                session.SaveChanges();
            }
        }
    }

    public class Customer
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CustomerSearchIndex : AbstractIndexCreationTask<Customer, CustomerSearchIndex.ReducedResult>
    {
        public CustomerSearchIndex()
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