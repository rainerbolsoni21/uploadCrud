using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(c => c.ProductId);
            Map(c => c.Name);
            Map(c => c.Description);
            Table("Product");
        }
    }
}
