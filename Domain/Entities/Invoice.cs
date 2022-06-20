using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Models
{
    public partial class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
