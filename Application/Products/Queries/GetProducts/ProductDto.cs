using Application.Common.Mappings;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetProducts
{
    public class ProductDto : IMapFrom<Product>
    {
       

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }

}
