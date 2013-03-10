using System.Web.Mvc;
using Raven.Client;
using RavenDemo.Web.Models;

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
            var user = _session.Load<User>(1);
            return View(user);
        }
    }
}