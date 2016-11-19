using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Application
{
    public class CompanyConfig
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Company config key cannot be empty")]
        [Index("CompanyConfigKey", IsUnique = true, Order = 1)]
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ConfigKey { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ConfigValue { get; set; }

        //[Timestamp]
        //public byte[] RowVersion { get; set; }

        public bool Active { get; set; }

        [Index("CompanyConfigKey", IsUnique = true, Order = 2)]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        [Index("CompanyConfigKey", IsUnique = true, Order = 3)]
        [ForeignKey("Period")]
        public int? PeriodId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Period Period { get; set; }
    }
}