using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Core.Models
{
    public partial class _10X_StagingContext : DbContext, I10XStagingDbContext
    {
        public _10X_StagingContext()
        {
        }
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;
        public _10X_StagingContext(
      DbContextOptions<_10X_StagingContext> options,
      ICurrentUserService currentUserService,
      IDomainEventService domainEventService,
      IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }


        //public virtual DbSet<TCall> TCalls { get; set; }
        public virtual DbSet<TCall> TCalls { get; set; }

        public virtual DbSet<TCompany> Companies { get; set; }

        public virtual DbSet<CasCompanySetting> CasCompanySettings { get; set; }
        public virtual DbSet<CasLightningParameter> CasLightningParameters { get; set; }

        //DbSet<TCall> I10XStagingDbContext.TCalls => Set<TCall>();
        // DbSet<Domain.entities.TCall> I10XStagingDbContext.TCalls { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=RMODH-0647;Database=10X_Staging;Trusted_Connection=True;");
            //}
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(_10X_StagingContext).Assembly);

            //modelBuilder.Entity<TCall>(entity =>
            //{
            //    entity.ToTable("t_Call");

            //    entity.HasIndex(e => new { e.ClientName, e.ThrowTime }, "IDX_NC_t_Call_ClientNameThrowtime");

            //    entity.HasIndex(e => new { e.CallListId, e.IsConversation }, "IDX_NC_t_call_CallListIdIsConversation");

            //    entity.HasIndex(e => new { e.Id, e.ThrowTime }, "IDX_NC_t_call_IDThrowTime");

            //    entity.HasIndex(e => new { e.ThrowTime, e.ProblemCodes }, "IDX_NC_t_call_ThrowtimeProblemCodes");

            //    entity.HasIndex(e => e.ClientName, "IX_t_Call");

            //    entity.HasIndex(e => e.LeadId, "IX_t_Call_AttemptedLeads");

            //    entity.HasIndex(e => e.CampaignName, "IX_t_Call_CampaignName");

            //    entity.HasIndex(e => e.ClientName, "IX_t_Call_ClientName");

            //    entity.HasIndex(e => e.ContactId, "IX_t_Call_Contactid");

            //    entity.HasIndex(e => e.CreateDate, "IX_t_Call_CreateDate");

            //    entity.HasIndex(e => e.LeadTransitId, "IX_t_Call_LinkID");

            //    entity.HasIndex(e => e.SessionId, "IX_t_Call_SessionID");

            //    entity.HasIndex(e => new { e.ThrowTime, e.EndTime }, "IX_t_Call_ThrowTime_EndTime");

            //    entity.HasIndex(e => e.UserName, "IX_t_Call_UserName");

            //    entity.HasIndex(e => e.UserRole, "IX_t_Call_UserRole");

            //    entity.HasIndex(e => e.VoicemailLeft, "IX_t_Call_VoiceMailLeft");

            //    entity.HasIndex(e => e.ContactTel1, "NonClusteredIndex-20150817-021414");

            //    entity.HasIndex(e => e.ContactTel2, "NonClusteredIndex-20150817-021513");

            //    entity.HasIndex(e => new { e.ContactId, e.Id, e.CreateDate, e.CallListId }, "_dta_index_t_Call_5_1330103779__K80_K1_K62");

            //    entity.HasIndex(e => new { e.IsValidConnect, e.ClientName, e.Id, e.EndTime, e.CampaignName, e.DispositionText }, "_dta_index_t_Call_6_1330103779__K77_K4_K1_K13_K7_K22_69");

            //    entity.HasIndex(e => new { e.Id, e.ThrowTime }, "_dta_index_t_Call_7_1330103779__K1_K10");

            //    entity.HasIndex(e => new { e.CallListId, e.DispositionText, e.ThrowTime, e.ClientName, e.UserName, e.SessionId, e.CampaignName }, "_dta_index_t_Call_7_1330103779__K81_K22_K10_K4_K5_K73_K7_1_77_83");

            //    entity.HasIndex(e => e.ClientName, "idx_SS_t_Call_ClientName");

            //    entity.HasIndex(e => new { e.UserName, e.ThrowTime }, "idx_SS_t_Call_Username_Throwtime");

            //    entity.Property(e => e.Id).HasColumnName("ID");

            //    entity.Property(e => e.ActualLeadDisplayTime).HasColumnType("datetime");

            //    entity.Property(e => e.CachedLeadDisplayTime).HasColumnType("datetime");

            //    entity.Property(e => e.CallAlert)
            //        .HasMaxLength(500)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CallSendTime).HasColumnType("datetime");

            //    entity.Property(e => e.CampaignName)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ClientName)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyAddr1)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyAddr2)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyCity)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyCountry)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyExt1)
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyExt2)
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyInfo).IsUnicode(false);

            //    entity.Property(e => e.CompanyName)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyState)
            //        .HasMaxLength(2)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyTel1)
            //        .HasMaxLength(40)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyTel2)
            //        .HasMaxLength(40)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CompanyUrl)
            //        .HasMaxLength(100)
            //        .IsUnicode(false)
            //        .HasColumnName("CompanyURL");

            //    entity.Property(e => e.CompanyZip)
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactAddr1)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactAddr2)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactBackground).IsUnicode(false);

            //    entity.Property(e => e.ContactCity)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactCountry)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactDept)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactEmail)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactExt1)
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactExt2)
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactExt3)
            //        .HasMaxLength(40)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactFirstName)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactJobTitle)
            //        .HasMaxLength(1000)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactLastName)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactSalutation)
            //        .HasMaxLength(10)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactState)
            //        .HasMaxLength(2)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactTel1)
            //        .HasMaxLength(40)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactTel2)
            //        .HasMaxLength(40)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactTel3)
            //        .HasMaxLength(40)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ContactZip)
            //        .HasMaxLength(20)
            //        .IsUnicode(false);

            //    entity.Property(e => e.CreateDate)
            //        .HasColumnType("datetime")
            //        .HasDefaultValueSql("(getdate())");

            //    entity.Property(e => e.CreateUser)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false)
            //        .HasDefaultValueSql("('Unknown')");

            //    entity.Property(e => e.CrmId)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.DialedProbableDirectNumber)
            //        .HasMaxLength(40)
            //        .IsUnicode(false);

            //    entity.Property(e => e.DispositionText)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.EndTime).HasColumnType("datetime");

            //    entity.Property(e => e.EngineCallConnectTime).HasColumnType("datetime");

            //    entity.Property(e => e.EngineCallSignalTime).HasColumnType("datetime");

            //    entity.Property(e => e.IsColv).HasColumnName("IsCOLV");

            //    entity.Property(e => e.IsConnectOnHello).HasDefaultValueSql("((0))");

            //    entity.Property(e => e.IsDeleted)
            //        .IsRequired()
            //        .HasDefaultValueSql("((1))");

            //    entity.Property(e => e.IsDeletedInCrm).HasColumnName("IsDeletedInCRM");

            //    entity.Property(e => e.LeadCatchTime).HasColumnType("datetime");

            //    entity.Property(e => e.LeadListName)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.LeadSendTime).HasColumnType("datetime");

            //    entity.Property(e => e.ModDate)
            //        .HasColumnType("datetime")
            //        .HasDefaultValueSql("(getdate())");

            //    entity.Property(e => e.ModUser)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false)
            //        .HasDefaultValueSql("('Unknown')");

            //    entity.Property(e => e.NameCatchTime).HasColumnType("datetime");

            //    entity.Property(e => e.NoteText).IsUnicode(false);

            //    entity.Property(e => e.PhoneSysConnectTime).HasColumnType("datetime");

            //    entity.Property(e => e.PopupTime).HasColumnType("datetime");

            //    entity.Property(e => e.ProblemComments).IsUnicode(false);

            //    entity.Property(e => e.Referrer)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false)
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.RowVersion)
            //        .IsRequired()
            //        .IsRowVersion()
            //        .IsConcurrencyToken();

            //    entity.Property(e => e.SessionName)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.SpecialInstructions)
            //        .HasMaxLength(200)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ThrowTime).HasColumnType("datetime");

            //    entity.Property(e => e.UserName)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);
            //});

            OnModelCreatingPartial(modelBuilder);
        }
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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

        //    modelBuilder.Entity<TCall>(entity =>
        //    {
        //        entity.ToTable("t_Call");

        //        entity.HasIndex(e => new { e.ClientName, e.ThrowTime }, "IDX_NC_t_Call_ClientNameThrowtime");

        //        entity.HasIndex(e => new { e.CallListId, e.IsConversation }, "IDX_NC_t_call_CallListIdIsConversation");

        //        entity.HasIndex(e => new { e.Id, e.ThrowTime }, "IDX_NC_t_call_IDThrowTime");

        //        entity.HasIndex(e => new { e.ThrowTime, e.ProblemCodes }, "IDX_NC_t_call_ThrowtimeProblemCodes");

        //        entity.HasIndex(e => e.ClientName, "IX_t_Call");

        //        entity.HasIndex(e => e.LeadId, "IX_t_Call_AttemptedLeads");

        //        entity.HasIndex(e => e.CampaignName, "IX_t_Call_CampaignName");

        //        entity.HasIndex(e => e.ClientName, "IX_t_Call_ClientName");

        //        entity.HasIndex(e => e.ContactId, "IX_t_Call_Contactid");

        //        entity.HasIndex(e => e.CreateDate, "IX_t_Call_CreateDate");

        //        entity.HasIndex(e => e.LeadTransitId, "IX_t_Call_LinkID");

        //        entity.HasIndex(e => e.SessionId, "IX_t_Call_SessionID");

        //        entity.HasIndex(e => new { e.ThrowTime, e.EndTime }, "IX_t_Call_ThrowTime_EndTime");

        //        entity.HasIndex(e => e.UserName, "IX_t_Call_UserName");

        //        entity.HasIndex(e => e.UserRole, "IX_t_Call_UserRole");

        //        entity.HasIndex(e => e.VoicemailLeft, "IX_t_Call_VoiceMailLeft");

        //        entity.HasIndex(e => e.ContactTel1, "NonClusteredIndex-20150817-021414");

        //        entity.HasIndex(e => e.ContactTel2, "NonClusteredIndex-20150817-021513");

        //        entity.HasIndex(e => new { e.ContactId, e.Id, e.CreateDate, e.CallListId }, "_dta_index_t_Call_5_1330103779__K80_K1_K62");

        //        entity.HasIndex(e => new { e.IsValidConnect, e.ClientName, e.Id, e.EndTime, e.CampaignName, e.DispositionText }, "_dta_index_t_Call_6_1330103779__K77_K4_K1_K13_K7_K22_69");

        //        entity.HasIndex(e => new { e.Id, e.ThrowTime }, "_dta_index_t_Call_7_1330103779__K1_K10");

        //        entity.HasIndex(e => new { e.CallListId, e.DispositionText, e.ThrowTime, e.ClientName, e.UserName, e.SessionId, e.CampaignName }, "_dta_index_t_Call_7_1330103779__K81_K22_K10_K4_K5_K73_K7_1_77_83");

        //        entity.HasIndex(e => e.ClientName, "idx_SS_t_Call_ClientName");

        //        entity.HasIndex(e => new { e.UserName, e.ThrowTime }, "idx_SS_t_Call_Username_Throwtime");

        //        entity.Property(e => e.Id).HasColumnName("ID");

        //        entity.Property(e => e.ActualLeadDisplayTime).HasColumnType("datetime");

        //        entity.Property(e => e.CachedLeadDisplayTime).HasColumnType("datetime");

        //        entity.Property(e => e.CallAlert)
        //            .HasMaxLength(500)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CallSendTime).HasColumnType("datetime");

        //        entity.Property(e => e.CampaignName)
        //            .HasMaxLength(100)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ClientName)
        //            .HasMaxLength(100)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CompanyAddr1)
        //            .HasMaxLength(100)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CompanyAddr2)
        //            .HasMaxLength(100)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CompanyCity)
        //            .HasMaxLength(50)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CompanyCountry)
        //            .HasMaxLength(50)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CompanyExt1)
        //            .HasMaxLength(20)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CompanyExt2)
        //            .HasMaxLength(20)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CompanyInfo).IsUnicode(false);

        //        entity.Property(e => e.CompanyName)
        //            .HasMaxLength(100)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CompanyState)
        //            .HasMaxLength(2)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CompanyTel1)
        //            .HasMaxLength(40)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CompanyTel2)
        //            .HasMaxLength(40)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CompanyUrl)
        //            .HasMaxLength(100)
        //            .IsUnicode(false)
        //            .HasColumnName("CompanyURL");

        //        entity.Property(e => e.CompanyZip)
        //            .HasMaxLength(20)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactAddr1)
        //            .HasMaxLength(100)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactAddr2)
        //            .HasMaxLength(100)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactBackground).IsUnicode(false);

        //        entity.Property(e => e.ContactCity)
        //            .HasMaxLength(50)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactCountry)
        //            .HasMaxLength(50)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactDept)
        //            .HasMaxLength(50)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactEmail)
        //            .HasMaxLength(100)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactExt1)
        //            .HasMaxLength(20)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactExt2)
        //            .HasMaxLength(20)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactExt3)
        //            .HasMaxLength(40)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactFirstName)
        //            .HasMaxLength(50)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactJobTitle)
        //            .HasMaxLength(1000)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactLastName)
        //            .HasMaxLength(50)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactSalutation)
        //            .HasMaxLength(10)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactState)
        //            .HasMaxLength(2)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactTel1)
        //            .HasMaxLength(40)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactTel2)
        //            .HasMaxLength(40)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactTel3)
        //            .HasMaxLength(40)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ContactZip)
        //            .HasMaxLength(20)
        //            .IsUnicode(false);

        //        entity.Property(e => e.CreateDate)
        //            .HasColumnType("datetime")
        //            .HasDefaultValueSql("(getdate())");

        //        entity.Property(e => e.CreateUser)
        //            .IsRequired()
        //            .HasMaxLength(100)
        //            .IsUnicode(false)
        //            .HasDefaultValueSql("('Unknown')");

        //        entity.Property(e => e.CrmId)
        //            .HasMaxLength(100)
        //            .IsUnicode(false);

        //        entity.Property(e => e.DialedProbableDirectNumber)
        //            .HasMaxLength(40)
        //            .IsUnicode(false);

        //        entity.Property(e => e.DispositionText)
        //            .HasMaxLength(50)
        //            .IsUnicode(false);

        //        entity.Property(e => e.EndTime).HasColumnType("datetime");

        //        entity.Property(e => e.EngineCallConnectTime).HasColumnType("datetime");

        //        entity.Property(e => e.EngineCallSignalTime).HasColumnType("datetime");

        //        entity.Property(e => e.IsColv).HasColumnName("IsCOLV");

        //        entity.Property(e => e.IsConnectOnHello).HasDefaultValueSql("((0))");

        //        entity.Property(e => e.IsDeleted)
        //            .IsRequired()
        //            .HasDefaultValueSql("((1))");

        //        entity.Property(e => e.IsDeletedInCrm).HasColumnName("IsDeletedInCRM");

        //        entity.Property(e => e.LeadCatchTime).HasColumnType("datetime");

        //        entity.Property(e => e.LeadListName)
        //            .HasMaxLength(100)
        //            .IsUnicode(false);

        //        entity.Property(e => e.LeadSendTime).HasColumnType("datetime");

        //        entity.Property(e => e.ModDate)
        //            .HasColumnType("datetime")
        //            .HasDefaultValueSql("(getdate())");

        //        entity.Property(e => e.ModUser)
        //            .IsRequired()
        //            .HasMaxLength(100)
        //            .IsUnicode(false)
        //            .HasDefaultValueSql("('Unknown')");

        //        entity.Property(e => e.NameCatchTime).HasColumnType("datetime");

        //        entity.Property(e => e.NoteText).IsUnicode(false);

        //        entity.Property(e => e.PhoneSysConnectTime).HasColumnType("datetime");

        //        entity.Property(e => e.PopupTime).HasColumnType("datetime");

        //        entity.Property(e => e.ProblemComments).IsUnicode(false);

        //        entity.Property(e => e.Referrer)
        //            .IsRequired()
        //            .HasMaxLength(100)
        //            .IsUnicode(false)
        //            .HasDefaultValueSql("('')");

        //        entity.Property(e => e.RowVersion)
        //            .IsRequired()
        //            .IsRowVersion()
        //            .IsConcurrencyToken();

        //        entity.Property(e => e.SessionName)
        //            .HasMaxLength(100)
        //            .IsUnicode(false);

        //        entity.Property(e => e.SpecialInstructions)
        //            .HasMaxLength(200)
        //            .IsUnicode(false);

        //        entity.Property(e => e.ThrowTime).HasColumnType("datetime");

        //        entity.Property(e => e.UserName)
        //            .HasMaxLength(100)
        //            .IsUnicode(false);
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

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
