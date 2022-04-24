using System.Collections.Generic;
using Framework.Domain;

namespace Marketer.Domain.Entities.Products
{
    public class PackageType : EntityBase
	{
        public string Title { get; private set; }

        public List<Product> Products { get; set; }

        public PackageType(string title) => Title = title;

        public void Edit(string title) => Title = title;
    }
}