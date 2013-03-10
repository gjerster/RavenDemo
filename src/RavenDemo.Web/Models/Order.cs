using System;
using System.Collections.Generic;

namespace RavenDemo.Web.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public IList<OrderLine> OrderLines { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public DateTime Created { get; set; }
    }
}