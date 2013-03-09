using System.Collections.Generic;
using Raven.Client;
using Raven.Client.Document;
using RavenDemo.Tests.Domain;
using Xunit;

namespace RavenDemo.Tests
{
    public class OrderTests
    {
        [Fact]
        public void CanCreateOrder()
        {
            var store = new DocumentStore {Url = "http://localhost:8085", DefaultDatabase = "DemoTest"};
            store.Initialize();

            using (IDocumentSession session = store.OpenSession())
            {
                var lines = new List<OrderLine> {new OrderLine("1111", 1, "Product1")};
                var order = new Order("hgh", new ShippingAddress(), lines);
                session.Store(order);
                session.SaveChanges();
            }
        }
    }
}