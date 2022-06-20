using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Core.Identity;
using Domain.Common;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

#nullable disable

namespace Core.Models
{
    public partial class StoreDBContext : DbContext, IStoreDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;
        public StoreDBContext(
      DbContextOptions<StoreDBContext> options,
      ICurrentUserService currentUserService,
      IDomainEventService domainEventService,
      IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        //public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        //public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        //public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        //public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        DbSet<Product> IStoreDbContext.Products => Set<Product>();

        //public DbSet<AspNetRole> AspNetRoles => Set<AspNetRole>();

        public DbSet<AspNetUser> AspNetUsers => Set<AspNetUser>();

        //public DbSet<AspNetRoleClaim> AspNetRoleClaims => Set<AspNetRoleClaim>();

        //public DbSet<AspNetUserToken> AspNetUserToken => Set<AspNetUserToken>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        // entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedBy = "1";
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "1";
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var events = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents(events);

            return result;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=RMODH-0647;Database=StoreDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDBContext).Assembly);
            modelBuilder.Entity<AspNetUserLogin>().HasNoKey();
            modelBuilder.Ignore<AspNetUserClaim>();
            modelBuilder.Ignore<AspNetUserLogin>();
            modelBuilder.Ignore<AspNetUserRole>();
            modelBuilder.Ignore<AspNetUserToken>();
            modelBuilder.Ignore<AspNetRoleClaim>();
            
            //modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            //modelBuilder.Entity<AspNetRole>(entity =>
            //{
            //    entity.HasKey(e=>e.Id);
            //});
            ////modelBuilder.Entity<AspNetRole>(entity =>
            ////{
            ////    entity.Property(e => e.Name).HasMaxLength(256);

            ////    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            ////});
            //modelBuilder.Entity<AspNetUserRole>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.RoleId });
            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserRoles)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetRoleClaim>(entity =>
            //{
            //    entity.HasKey(e => e.RoleId);
            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetRoleClaims)
            //        .HasForeignKey(d => d.RoleId);

            //});
            ////modelBuilder.Entity<AspNetRoleClaim>(entity =>
            ////{
            ////    entity.Property(e => e.RoleId)
            ////        .IsRequired()
            ////        .HasMaxLength(450);

            ////    entity.HasOne(d => d.Role)
            ////        .WithMany(p => p.AspNetRoleClaims)
            ////        .HasForeignKey(d => d.RoleId);
            ////});
            //modelBuilder.Entity<AspNetUser>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //});
            ////modelBuilder.Entity<AspNetUser>(entity =>
            ////{
            ////    entity.Property(e => e.Email).HasMaxLength(256);

            ////    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            ////    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            ////    entity.Property(e => e.UserName).HasMaxLength(256);
            ////});
            //modelBuilder.Entity<AspNetUserClaim>(entity =>
            //{
            //    entity.HasKey(e => e.UserId);
            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId);
            //});
            ////modelBuilder.Entity<AspNetUserClaim>(entity =>
            ////{
            ////    entity.Property(e => e.UserId)
            ////        .IsRequired()
            ////        .HasMaxLength(450);

            ////    entity.HasOne(d => d.User)
            ////        .WithMany(p => p.AspNetUserClaims)
            ////        .HasForeignKey(d => d.UserId);
            ////});
            //modelBuilder.Entity<AspNetUserLogin>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            //    entity.HasOne(d => d.User)
            //                .WithMany(p => p.AspNetUserLogins)
            //        .HasForeignKey(d => d.UserId);
            //});

            ////modelBuilder.Entity<AspNetUserLogin>(entity =>
            ////{
            ////    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            ////    entity.Property(e => e.UserId)
            ////        .IsRequired()
            ////        .HasMaxLength(450);

            ////    entity.HasOne(d => d.User)
            ////        .WithMany(p => p.AspNetUserLogins)
            ////        .HasForeignKey(d => d.UserId);
            ////});



            ////modelBuilder.Entity<AspNetUserRole>(entity =>
            ////{
            ////    entity.HasKey(e => new { e.UserId, e.RoleId });

            ////    entity.HasOne(d => d.Role)
            ////        .WithMany(p => p.AspNetUserRoles)
            ////        .HasForeignKey(d => d.RoleId);

            ////    entity.HasOne(d => d.User)
            ////        .WithMany(p => p.AspNetUserRoles)
            ////        .HasForeignKey(d => d.UserId);
            ////});
            //modelBuilder.Entity<AspNetUserToken>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserTokens)
            //        .HasForeignKey(d => d.UserId);
            //});
            //modelBuilder.Entity<AspNetUserToken>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserTokens)
            //        .HasForeignKey(d => d.UserId);
            //});

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Invoice");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.InvoiceNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(20)
                    .HasColumnName("createdBy");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(20);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stock");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_ProductMaster");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("Vendor");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private async Task DispatchEvents(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }
    }
}
