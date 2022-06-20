using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Queries.Invoices
{
    public class InvoiceVm
    {
        public IList<InvoiceListDto> Lists { get; set; } = new List<InvoiceListDto>();
    }
}
