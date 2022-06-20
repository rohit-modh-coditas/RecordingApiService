using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetProducts
{
    public class GetProductsQuery : IRequest<ProductVm>
    {
        
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductsQuery, ProductVm>
    {
        private readonly IStoreDbContext _storeDbContext;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IStoreDbContext context, IMapper mapper)
        {
            _storeDbContext = context;
            _mapper = mapper;
        }
        public async Task<ProductVm> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {

            return new ProductVm
            {
                Lists = await _storeDbContext.Products.AsNoTracking().ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Name)
                .ToListAsync(cancellationToken)
            };

          

        }

    }
}
