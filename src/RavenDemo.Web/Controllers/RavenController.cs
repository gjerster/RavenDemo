using System;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Document;

namespace RavenDemo.Web.Controllers
{
    public class RavenController : Controller
    {
        private static readonly Lazy<IDocumentStore> LazyDocStore =
            new Lazy<IDocumentStore>(() =>
                {
                    var store = new DocumentStore
                        {
                            Url = "http://localhost:8085",
                            DefaultDatabase = "Demo"
                        };
                    store.Initialize();
                    return store;
                });

        public static IDocumentStore DocumentStore
        {
            get { return LazyDocStore.Value; }
        }

        public new IDocumentSession Session { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Session = DocumentStore.OpenSession();
            Session.Advanced.UseOptimisticConcurrency = true;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            using (Session)
            {
                if (Session == null)
                    return;
                if (filterContext.Exception != null)
                    return;
                Session.SaveChanges();
            }
        }
    }
}