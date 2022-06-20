using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Domain.Entities
{
    [Table("cas_LightningParameter")]
    public partial class CasLightningParameter
    {
        [Key]
        public int LightningParameterId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
