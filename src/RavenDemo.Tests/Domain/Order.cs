using System.Collections.Generic;
using System.Linq;

namespace RavenDemo.Tests.Domain
{
    public class Order
    {
        private readonly List<OrderLine> _orderLines;

        public Order(string userName, ShippingAddress shippingAddress, IEnumerable<OrderLine> orderLines)
        {
            UserName = userName;
            ShippingAddress = shippingAddress;
            _orderLines = orderLines.ToList();
        }

        public int Id { get; private set; }
        public string UserName { get; private set; }
        public ShippingAddress ShippingAddress { get; private set; }

        public IEnumerable<OrderLine> OrderLines
        {
            get { return _orderLines; }
        }
    }
}