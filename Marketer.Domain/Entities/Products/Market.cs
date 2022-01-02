using Framework.Domain;
using Marketer.Domain.Entities.Account;

namespace Marketer.Domain.Entities.Products
{
    public class Market : EntityBase
    {
        public long CityId { get; private set; }
        public long VisitorId { get; private set; }
        public string Name { get; private set; }
        public string Owner { get; private set; }
        public string MobilePhone { get; private set; }

        public City City { get; private set; }
        public Visitor Visitor { get; private set; }

        public Market(long cityId,long visitorId, string name, string owner, string mobilePhone)
        {
            CityId = cityId;
            VisitorId = visitorId;
            Name = name;
            Owner = owner;
            MobilePhone = mobilePhone;
        }

        public void Edit(long cityId,string name, string owner, string mobilePhone)
        {
            CityId = cityId;
            Name = name;
            Owner = owner;
            MobilePhone = mobilePhone;
        }

    }
}