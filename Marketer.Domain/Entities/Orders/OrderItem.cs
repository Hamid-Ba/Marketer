using Framework.Domain;
using Marketer.Domain.Entities.Products;

namespace Marketer.Domain.Entities.Orders
{
    public class OrderItem : EntityBase
    {
        public long OrderId { get; private set; }
        public long ProductId { get; private set; }
        public double PayAmount { get; private set; }
        public double DiscountPrice { get; private set; }
        public int Count { get; private set; }

        public Order Order { get; private set; }
        public Product Product { get; private set; }

        public OrderItem(long orderId, long productId, int count)
        {
            OrderId = orderId;
            ProductId = productId;
            Count = count;
        }
    }
}