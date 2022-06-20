using Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Invoices.Queries.Invoices
{
    public class InvoiceItemDto: IMapFrom<InvoiceItem>
    {
        public int ID { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<InvoiceItem, InvoiceItemDto>()
                .ForMember(d => d.ID, opt => opt.MapFrom(s => (int)s.Priority));
        }

    }
}
