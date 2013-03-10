using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Raven.Client;
using Raven.Client.Document;

namespace RavenDemo.Web.App_Start
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => HttpContext.Current.User).InstancePerHttpRequest();
            builder.RegisterModule(new RavenDbModule());
            builder.RegisterControllers(typeof (MvcApplication).Assembly);
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }

    public class RavenDbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            IDocumentStore store = new DocumentStore {ConnectionStringName = "demo"};
            store.Initialize();
            builder.RegisterInstance(store).SingleInstance();
            builder.Register(c => c.Resolve<IDocumentStore>().OpenSession()).InstancePerLifetimeScope();
        }
    }
}