using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Document;
using RavenDemo.Web.Models;

namespace RavenDemo.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly DocumentStore _store;

        public UserController()
        {
            _store = new DocumentStore {Url = "http://localhost:8085", DefaultDatabase = "demo"};
            _store.Initialize();
        }

        public ActionResult Index()
        {
            using (IDocumentSession session = _store.OpenSession())
            {
                List<User> users = session.Query<User>().ToList();
                return View(users);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User model)
        {
            using (IDocumentSession session = _store.OpenSession())
            {
                var user = new User {Username = model.Username, Name = model.Name};
                session.Store(user);
                session.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}