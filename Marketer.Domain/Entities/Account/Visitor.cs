using Framework.Domain;
using Marketer.Domain.Entities.Products;
using System;
using System.Collections.Generic;

namespace Marketer.Domain.Entities.Account
{
    public class Visitor : EntityBase
    {
        public string UniqueCode { get; private set; }
        public string FullName { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }
        public bool IsBlock { get; private set; }
        public long PlacedOrderCount { get; private set; }

        public List<Market> Markets { get; private set; }

        public Visitor(string uniqueCode, string fullName, string mobile, string password)
        {
            UniqueCode = uniqueCode;
            FullName = fullName;
            Mobile = mobile;
            Password = password;
            IsBlock = false;
            PlacedOrderCount = 0;
        }

        public void Edit(string uniqueCode, string fullName, string mobile, string password)
        {
            UniqueCode = uniqueCode;
            FullName = fullName;
            Mobile = mobile;

            if (!string.IsNullOrWhiteSpace(password))
                Password = password;

            LastUpdateDate = DateTime.Now;
        }

        public void BlockPrccess(bool isBlock) => IsBlock = isBlock;

        public void OneOrderPlaced() => PlacedOrderCount++;
    }
}