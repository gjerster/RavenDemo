using System.Web.Mvc;
using Raven.Client.Document;

namespace RavenDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            var store = new DocumentStore {Url = "http://localhost:8085", DefaultDatabase = "demo"};
            store.Initialize();
        }

        public ActionResult Index()
        {


            return View();
        }
    }
}