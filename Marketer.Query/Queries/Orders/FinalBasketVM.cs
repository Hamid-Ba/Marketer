namespace Marketer.Query.Queries.Orders
{
    public class FinalBasketVM
    {
        public long OrderId { get; set; }
        public long VisitorId { get; set; }
        public long MarketId { get; set; }
        public double PayAmount { get; set; }
        public double TotalDiscount { get; set; }
        public double TotalPrice { get; set; }
    }
}
