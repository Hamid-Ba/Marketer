using Framework.Domain;
using System;

namespace Marketer.Application.Contract.ViewModels.Orders
{
    public class OrderVM
    {
        public long Id { get; set; }
        public long VisitorId { get;  set; }
        public long MarketId { get;  set; }
        public double TotalPrice { get;  set; }
        public double TotalDiscount { get;  set; }
        public double PayAmount { get;  set; }
        public long RefId { get;  set; }
        public bool IsPayed { get;  set; }
        public DateTime PlaceOrderDate { get;  set; }
        public OrderStatus Status { get;  set; }
    }

    public class OrderItemVM
    {
        public long Id { get; set; }
        public long OrderId { get;  set; }
        public long ProductId { get;  set; }
        public double PayAmount { get;  set; }
        public double DiscountPrice { get;  set; }
        public int Count { get;  set; }
    }
}