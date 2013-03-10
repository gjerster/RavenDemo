using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Document;
using RavenDemo.Web.Models;

namespace RavenDemo.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IDocumentStore _store;

        public OrderController()
        {
            _store = new DocumentStore {Url = "http://localhost:8085", DefaultDatabase = "demo"};
            _store.Initialize();
        }

        public ActionResult Index(int id = 1)
        {
            using (IDocumentSession session = _store.OpenSession())
            {
                RavenQueryStatistics stats;
                const int pageSize = 10;
                List<Order> orders = session
                    .Query<Order>()
                    .Statistics(out stats)
                    .Take(pageSize)
                    .Skip((id - 1)*pageSize)
                    .ToList();
                var pagedList = new PagedList<Order>(id, pageSize, stats.TotalResults, orders);
                return View(pagedList);
            }
        }

        public ActionResult Details(string id)
        {
            using (IDocumentSession session = _store.OpenSession())
            {
                var order = session.Load<Order>(id);
                return View(order);
            }
        }
    }
}