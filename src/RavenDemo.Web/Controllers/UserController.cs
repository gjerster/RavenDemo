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
                List<User> users = session.Query<User>().Take(10).ToList();
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
                session.Store(model);
                session.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            using (IDocumentSession session = _store.OpenSession())
            {
                var model = session.Load<User>(id);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            using (IDocumentSession session = _store.OpenSession())
            {
                session.Store(model);
                session.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Paging(int id=1)
        {
            using (IDocumentSession session = _store.OpenSession())
            {
                RavenQueryStatistics stats;
                const int pageSize = 10;
                List<User> users = session
                    .Query<User>()
                    .Statistics(out stats)
                    .Take(pageSize)
                    .Skip((id - 1)*pageSize)
                    .ToList();
                var pagedList = new PagedList<User>(id, pageSize, stats.TotalResults, users);
                return View(pagedList);
            }
        }
    }
}