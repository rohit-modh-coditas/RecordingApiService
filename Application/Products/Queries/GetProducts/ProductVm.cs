using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetProducts
{
    public class ProductVm
    {
       // public IList<PriorityLevelDto> PriorityLevels { get; set; } = new List<PriorityLevelDto>();

        public IList<ProductDto> Lists { get; set; } = new List<ProductDto>();
    }
}
