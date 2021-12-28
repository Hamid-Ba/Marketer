using Framework.Domain;
using Marketer.Domain.Entities.Account;

namespace Marketer.Domain.Entities.Products
{
    public class Market : EntityBase
    {
        public long VisitorId { get; private set; }
        public string Name { get; private set; }
        public string Owner { get; private set; }
        public string MobilePhone { get; private set; }

        public Visitor Visitor { get; private set; }

        public Market(long visitorId, string name, string owner, string mobilePhone)
        {
            VisitorId = visitorId;
            Name = name;
            Owner = owner;
            MobilePhone = mobilePhone;
        }

        public void Edit(string name, string owner, string mobilePhone)
        {
            Name = name;
            Owner = owner;
            MobilePhone = mobilePhone;
        }

    }
}