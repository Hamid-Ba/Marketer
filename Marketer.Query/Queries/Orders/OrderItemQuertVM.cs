using Marketer.Query.Queries.Products;

namespace Marketer.Query.Queries.Orders
{
    public class OrderItemQuertVM
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public double PayAmount { get; set; }
        public double DiscountPrice { get; set; }
        public int Count { get; set; }
        public ProductQueryVM Product { get; set; }
    }
}
