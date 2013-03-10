using System.Web.Mvc;
using Raven.Client;

namespace RavenDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocumentSession _session;

        public HomeController(IDocumentSession session)
        {
            _session = session;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}