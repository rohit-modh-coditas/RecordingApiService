using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Models
{
    public partial class Product
    {
        public Product()
        {
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
