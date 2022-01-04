using Framework.Domain;
using System;
using System.Collections.Generic;

namespace Marketer.Query.Queries.Orders
{
    public class OrderQueryVM
    {
        public long Id { get; set; }
        public long VisitorId { get; set; }
        public long MarketId { get; set; }
        public double TotalPrice { get; set; }
        public double TotalDiscount { get; set; }
        public double PayAmount { get; set; }
        public long RefId { get; set; }
        public bool IsPayed { get; set; }
        public int ItemsCount { get; set; }
        public DateTime PlaceOrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemQuertVM> Items { get; set; }
    }
}
