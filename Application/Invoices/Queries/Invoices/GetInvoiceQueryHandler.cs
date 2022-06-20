using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Invoices.Queries.Invoices
{
    public class GetInvoiceQuery:IRequest<InvoiceVm>
    {

    }
    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, InvoiceVm>
    {
        private readonly IStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetInvoiceQueryHandler(IStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<InvoiceVm> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            //DbSet properties
 
            throw new NotImplementedException();
        }
    }
}
