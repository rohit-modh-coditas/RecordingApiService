using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Commands.CreateProductCommand
{
    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        private readonly IStoreDbContext _context;
        public CreateProductCommandValidator(IStoreDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 10 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.Products
                .AllAsync(l => l.Name != title, cancellationToken);
        }
    }
}
