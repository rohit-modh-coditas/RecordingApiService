using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Domain.Entities
{
    public partial class CasCompanySetting
    {
        [Key]
        public int CompanySettingId { get; set; }
        public int CompanyId { get; set; }
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public string LastModUser { get; set; }
        public DateTime? LastModDate { get; set; }
        public int? OldId { get; set; }
        public int? EnvId { get; set; }

        public virtual TCompany Company { get; set; }
    }
}
