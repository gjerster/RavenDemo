using Raven.Client;
using Raven.Client.Embedded;
using Xunit;

namespace RavenDemo.Tests
{
    public class EmbeddableDocumentStoreTests
    {
        [Fact]
        public void CanAddUserToRaven()
        {
            var store = new EmbeddableDocumentStore {RunInMemory = true};
            store.Initialize();

            using (IDocumentSession session = store.OpenSession())
            {
                var user = new User {Name = "Test"};
                session.Store(user);
                session.SaveChanges();
                Assert.NotNull(user.Id);
                Assert.True(user.Id.Contains("users/"));
            }
        }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}