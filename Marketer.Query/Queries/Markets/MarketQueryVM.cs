namespace Marketer.Query.Queries.Markets
{
    public class MarketQueryVM
    {
        public long Id { get; set; }
        public long CityId { get;  set; }
        public long VisitorId { get;  set; }
        public string VisitorName { get; set; }
        public string VisitorCode { get; set; }
        public string CityName { get; set; }
        public string Name { get;  set; }
        public string Owner { get;  set; }
        public string MobilePhone { get;  set; }
        public string MarketWithCity { get; set; }
        public string Address { get; set; }
    }
}