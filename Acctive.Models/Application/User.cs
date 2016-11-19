using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Application
{
    public class User
    {
        public User()
        {
            Active = true;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "User id cannot be empty")]
        [Index("UserId", IsUnique = true)]
        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string UserId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Password { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ImageFilePath { get; set; }

        public bool Active { get; set; }

        public virtual List<Role> Roles { get; set; }
    }
}