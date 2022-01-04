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
        public long RefId { get; private set; }
        public bool IsPayed { get; private set; }
        public DateTime PlaceOrderDate { get; private set; }
        public OrderStatus Status { get; private set; }

        public Visitor Visitor { get; private set; }
        public Market Market { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }

        public Order(long visitorId)
        {
            VisitorId = visitorId;
            OrderItems = new List<OrderItem>();
        }

        public Order(long visitorId, long marketId, double totalPrice, double totalDiscount, double payAmount, long refId, bool isPayed, DateTime placeOrderDate, OrderStatus status)
        {
            VisitorId = visitorId;
            MarketId = marketId;
            TotalPrice = totalPrice;
            TotalDiscount = totalDiscount;
            PayAmount = payAmount;
            RefId = refId;
            IsPayed = isPayed;
            PlaceOrderDate = placeOrderDate;
            Status = status;
        }
    }
}