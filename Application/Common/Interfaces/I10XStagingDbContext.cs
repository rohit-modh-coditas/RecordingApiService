
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface I10XStagingDbContext
    {
         DbSet<TCall> TCalls { get;}
        DbSet<TCompany> Companies { get; }
        DbSet<CasCompanySetting> CasCompanySettings { get; }
        DbSet<CasLightningParameter> CasLightningParameters { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
