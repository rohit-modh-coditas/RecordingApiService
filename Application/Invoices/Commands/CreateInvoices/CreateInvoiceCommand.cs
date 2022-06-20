using Application.Invoices.Queries.Invoices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Commands.CreateInvoices
{
    class CreateInvoiceCommand : IRequest<int>
    {
        public CreateInvoiceCommand()
        {
            this.InvoiceItems = new List<InvoiceItemVm>();
        }
        public string InvoiceNumber { get; set; }
        public string Logo { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public string PaymentTerms { get; set; }
        public DateTime? DueDate { get; set; }
        public double Discount { get; set; }
       // public DiscountType DiscountType { get; set; }
        public double Tax { get; set; }
       // public TaxType TaxType { get; set; }
        public double AmountPaid { get; set; }
        public IList<InvoiceItemVm> InvoiceItems { get; set; }
    }
}
