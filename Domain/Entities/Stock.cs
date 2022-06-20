using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Models
{
    public partial class Stock
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
