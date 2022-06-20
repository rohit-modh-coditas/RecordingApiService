using Application.Common.Mappings;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetProducts
{
    public class ProductListDto:IMapFrom<Product>
    {
        public ProductListDto()
        {
            Items = new List<ProductDto>();
        }

        public int Id { get; set; }

        public IList<ProductDto> Items { get; set; }
    }
}
