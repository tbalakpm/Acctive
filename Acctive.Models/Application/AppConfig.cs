using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Application
{
    public class AppConfig
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "AppConfig key cannot be empty")]
        [Index("AppConfigKey", IsUnique = true)]
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ConfigKey { get; set; }

        [StringLength(255)]
        public string ConfigValue { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public bool Active { get; set; }
    }
}