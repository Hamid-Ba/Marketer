using Framework.Domain;
using Marketer.Domain.Entities.Account;
using Marketer.Domain.Entities.Products;
using System;
using System.Collections.Generic;

namespace Marketer.Domain.Entities.Orders
{
    public class Order : EntityBase
    {
        public long VisitorId { get; private set; }
        public long MarketId { get; private set; }
        public double TotalPrice { get; private set; }
        public double TotalDiscount { get; private set; }
        public double PayAmount { get; private set; }
        public string RefId { get; private set; }
        public bool IsPayed { get; private set; }
        public DateTime PlaceOrderDate { get; private set; }
        public OrderStatus Status { get; private set; }

        public Visitor Visitor { get; private set; }
        public Market Market { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public Order(long visitorId)
        {
            IsPayed = false;
            VisitorId = visitorId;
            OrderItems = new List<OrderItem>();
        }

        public void AddItem(OrderItem item) => OrderItems.Add(item);

        public void PlaceOrder(long marketId, double totalPrice, double totalDiscount, double payAmount)
        {
            MarketId = marketId;
            TotalPrice = totalPrice;
            TotalDiscount = totalDiscount;
            PayAmount = payAmount;
            IsPayed = true;
            PlaceOrderDate = DateTime.Now;
            Status = OrderStatus.Prepreing;
            RefId = "O" + Guid.NewGuid().ToString().Substring(0, 7);
        }
    }
}