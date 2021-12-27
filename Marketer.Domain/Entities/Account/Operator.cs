using Framework.Domain;
using System;

namespace Marketer.Domain.Entities.Account
{
    public class Operator : EntityBase
    {
        public long RoleId { get; private set; }
        public string FullName { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }

        public Role Role { get; private set; }

        public Operator(string fullName, string mobile, string password)
        {
            FullName = fullName;
            Mobile = mobile;
            Password = password;
        }

        public void Edit(string fullName, string mobile, string password)
        {
            FullName = fullName;
            Mobile = mobile;
            Password = password;
            LastUpdateDate = DateTime.Now;
        }

    }
}