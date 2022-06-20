using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IStoreDbContext
    {
        DbSet<Product> Products { get; }
        //DbSet<AspNetRole> AspNetRoles { get; }
        DbSet<AspNetUser> AspNetUsers { get; }
        //DbSet<AspNetRoleClaim> AspNetRoleClaims { get; }
        //DbSet<AspNetUserToken> AspNetUserToken { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
