using Framework.Domain;

namespace Marketer.Domain.Entities.Extera
{
    public class ContactUs : EntityBase
    {
        public string FullName { get; private set; }
        public string MobilePhone { get; private set; }
        public string Message { get; private set; }

        public ContactUs(string fullName, string mobilePhone, string message)
        {
            FullName = fullName;
            MobilePhone = mobilePhone;
            Message = message;
        }
    }
}
