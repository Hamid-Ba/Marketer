using Framework.Application;
using Framework.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Marketer.Application.Contract.ViewModels.Orders
{
    public class OrderVM
    {
        public long Id { get; set; }
        public long VisitorId { get;  set; }
        public string VisitorName { get; set; }
        public long MarketId { get;  set; }
        public string MarketName { get; set; }
        public double TotalPrice { get;  set; }
        public double TotalDiscount { get;  set; }
        public double PayAmount { get;  set; }
        public string RefId { get;  set; }
        public bool IsPayed { get;  set; }
        public string Description { get; set; }
        public DateTime PlaceOrderDate { get;  set; }
        public OrderStatus Status { get;  set; }
    }

    public class OrderItemVM
    {
        public long Id { get; set; }
        public long OrderId { get;  set; }
        public long ProductId { get;  set; }
        public string ProductTitle { get;  set; }
        public double PayAmount { get;  set; }
        public double DiscountPrice { get;  set; }
        public int Count { get;  set; }
    }

    public class ChangeStatusOrderVM
    {
        public long Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public OrderStatus Status { get; set; }
    }
}