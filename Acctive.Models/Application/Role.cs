using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Application
{
    public class Role
    {
        public Role()
        {
            Active = true;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Role name cannot be empty")]
        [Index("RoleName", IsUnique = true)]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Name { get; set; }

        public RoleType Type { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ImageFilePath { get; set; }

        public bool Active { get; set; }

        public virtual List<User> Users { get; set; }
    }

    public enum RoleType
    {
        Unknown,
        Guest,
        User,
        ReportsUser,
        Configurator,
        Manager,
        Administrator
    }
}