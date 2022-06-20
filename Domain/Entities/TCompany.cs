using System;
using System.Collections.Generic;

#nullable disable

namespace Domain.Entities
{
    public partial class TCompany
    {
        public TCompany()
        {
            //CasAgentDispositionMaps = new HashSet<CasAgentDispositionMap>();
            //CasCompanyDispositions = new HashSet<CasCompanyDisposition>();
            CasCompanySettings = new HashSet<CasCompanySetting>();
            //CasFieldPriorities = new HashSet<CasFieldPriority>();
            //CasGoals = new HashSet<CasGoal>();
            //CasListTypes = new HashSet<CasListType>();
            //CasMappings = new HashSet<CasMapping>();
            //CasNonBaseMappingFields = new HashSet<CasNonBaseMappingField>();
            //CasPbrules = new HashSet<CasPbrule>();
            //CasPriorityMappings = new HashSet<CasPriorityMapping>();
            //CasSessionSchedulings = new HashSet<CasSessionScheduling>();
            //CasTeams = new HashSet<CasTeam>();
            //CasTriggers = new HashSet<CasTrigger>();
            //CasUserInvitations = new HashSet<CasUserInvitation>();
            //TAccountHistories = new HashSet<TAccountHistory>();
            //TBilledSessions = new HashSet<TBilledSession>();
            //TCampaigns = new HashSet<TCampaign>();
            //TContracts = new HashSet<TContract>();
            //TDncs = new HashSet<TDnc>();
            //TRolesHistories = new HashSet<TRolesHistory>();
            //TScheduledSessions = new HashSet<TScheduledSession>();
            //TSessions = new HashSet<TSession>();
            //TUsers = new HashSet<TUser>();
        }

        public int Id { get; set; }
        public string Identifier { get; set; }
        public string CompanyName { get; set; }
        public int? Kind { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Tel1 { get; set; }
        public string Ext1 { get; set; }
        public string Tel2 { get; set; }
        public string Ext2 { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Overview { get; set; }
        public int? Employees { get; set; }
        public decimal? Revenue { get; set; }
        public decimal? Growth { get; set; }
        public bool? IsDeleted { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime ModDate { get; set; }
        public string ModUser { get; set; }
        public string Fax { get; set; }
        public string FaxExt { get; set; }
        public string CrmUrlFormat { get; set; }
        public string SalesRep { get; set; }
        public DateTime? SalesRepDate { get; set; }
        public string AccountMgr { get; set; }
        public DateTime? AccountMgrDate { get; set; }
        public string ClientSvcsMgr { get; set; }
        public DateTime? ClientSvcsMgrDate { get; set; }
        public string Memo { get; set; }
        public bool? IsDispositionRequired { get; set; }
        public bool? IsVoicemailEnabled { get; set; }
        public string TimeZone { get; set; }
        public int Priority { get; set; }
        public int Sla { get; set; }
        public string PreferredCrm { get; set; }
        public string CrmLoginMask { get; set; }
        public int? Colvpriority { get; set; }
        public int? Colvsla { get; set; }
        public string ScriptCompanyName { get; set; }
        public int? PermittedRoles { get; set; }
        public string SfaccountUrl { get; set; }
        public string SessionReportDl { get; set; }
        public string EngineIdentifier { get; set; }

        public virtual ICollection<CasCompanySetting> CasCompanySettings { get; set; }

        //public virtual ICollection<CasAgentDispositionMap> CasAgentDispositionMaps { get; set; }
        //public virtual ICollection<CasCompanyDisposition> CasCompanyDispositions { get; set; }
        //public virtual ICollection<CasFieldPriority> CasFieldPriorities { get; set; }
        //public virtual ICollection<CasGoal> CasGoals { get; set; }
        //public virtual ICollection<CasListType> CasListTypes { get; set; }
        //public virtual ICollection<CasMapping> CasMappings { get; set; }
        //public virtual ICollection<CasNonBaseMappingField> CasNonBaseMappingFields { get; set; }
        //public virtual ICollection<CasPbrule> CasPbrules { get; set; }
        //public virtual ICollection<CasPriorityMapping> CasPriorityMappings { get; set; }
        //public virtual ICollection<CasSessionScheduling> CasSessionSchedulings { get; set; }
        //public virtual ICollection<CasTeam> CasTeams { get; set; }
        //public virtual ICollection<CasTrigger> CasTriggers { get; set; }
        //public virtual ICollection<CasUserInvitation> CasUserInvitations { get; set; }
        //public virtual ICollection<TAccountHistory> TAccountHistories { get; set; }
        //public virtual ICollection<TBilledSession> TBilledSessions { get; set; }
        //public virtual ICollection<TCampaign> TCampaigns { get; set; }
        //public virtual ICollection<TContract> TContracts { get; set; }
        //public virtual ICollection<TDnc> TDncs { get; set; }
        //public virtual ICollection<TRolesHistory> TRolesHistories { get; set; }
        //public virtual ICollection<TScheduledSession> TScheduledSessions { get; set; }
        //public virtual ICollection<TSession> TSessions { get; set; }
        //public virtual ICollection<TUser> TUsers { get; set; }
    }
}
