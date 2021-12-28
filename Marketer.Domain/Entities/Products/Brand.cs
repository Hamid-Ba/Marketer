using Framework.Domain;
using System;

namespace Marketer.Domain.Entities.Products
{
    public class Brand : EntityBase
    {
        public string Name { get; private set; }

        //public List<Product> Products { get; private set; }

        public Brand(string name) => Name = name;

        public void Edit(string name)
        {
            Name = name;
            LastUpdateDate = DateTime.Now;
        }
    }
}
