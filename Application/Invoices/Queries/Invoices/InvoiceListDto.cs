using Application.Common.Mappings;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Queries.Invoices
{
    public class InvoiceListDto: IMapFrom<Invoice>
    {
        //some extra fields apart from invoice item entity
        public InvoiceListDto()
        {
            Items = new List<InvoiceItemDto>();
        }

        public IList<InvoiceItemDto> Items { get; set; }

    }
}
