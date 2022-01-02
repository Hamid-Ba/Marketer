using Framework.Domain;
using System;
using System.Collections.Generic;

namespace Marketer.Domain.Entities.Products
{
    public class City : EntityBase
    {
        public string Name { get; private set; }

        public List<Market> Markets { get; private set; }

        public City(string name) => Name = name;

        public void Edit(string name)
        {
            Name = name;
            LastUpdateDate = DateTime.Now;
        } 
    }
}