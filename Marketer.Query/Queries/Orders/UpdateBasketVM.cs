namespace Marketer.Query.Queries.Orders
{
    public class UpdateBasketVM
    {
        public long[] ItemsId { get; set; }
        public int[] Quantity { get; set; }
        public long[] ProductsId { get; set; }
        public long VisitorId { get; set; }
    }
}