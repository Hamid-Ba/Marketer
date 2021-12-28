using Framework.Domain;
using System;

namespace Marketer.Domain.Entities.Discounts
{
    public class Discount : EntityBase
    {
        public long ProductId { get; private set; }
        public int DiscountRate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; private set; }

        public Discount(long productId, int discountRate, DateTime startDate, DateTime endDate, string reason)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }

        public void Edit(long productId, int discountRate, DateTime startDate, DateTime endDate, string reason)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;

            LastUpdateDate = DateTime.Now;
        }
    }
}
