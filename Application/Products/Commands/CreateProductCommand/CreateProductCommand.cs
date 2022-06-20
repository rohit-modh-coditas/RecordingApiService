using Application.Common.Interfaces;
using Application.Products.Queries.GetProducts;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands.CreateProductCommand
{
    public class CreateProductCommand: IRequest<int>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IStoreDbContext _context;
        public CreateProductCommandHandler(IStoreDbContext context)
        {
            _context = context;

        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new Product();
            
            entity.Name = request.Name;
            entity.Description = request.Description;

            _context.Products.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
