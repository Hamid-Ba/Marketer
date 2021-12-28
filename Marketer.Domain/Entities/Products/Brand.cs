using Framework.Domain;
using System;
using System.Collections.Generic;

namespace Marketer.Domain.Entities.Products
{
    public class Brand : EntityBase
    {
        public string Name { get; private set; }
        public string KeyWords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }

        public List<Product> Products { get; private set; }

        public Brand(string name, string keyWords, string metaDescription, string slug)
        {
            Name = name;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;
        }

        public void Edit(string name, string keyWords, string metaDescription, string slug)
        {
            Name = name;
            KeyWords = keyWords;
            MetaDescription = metaDescription;
            Slug = slug;

            LastUpdateDate = DateTime.Now;
        }
    }
}
