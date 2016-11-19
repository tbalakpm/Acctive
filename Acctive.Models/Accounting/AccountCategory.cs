using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Accounting
{
    public class AccountCategory
    {
        public AccountCategory()
        {
            Active = true;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Account category code cannot be empty")]
        [Index("AcctCatCode", IsUnique = true)]
        [Column(TypeName = "varchar")]
        [StringLength(15)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Account category name cannot be empty")]
        [Index("AcctCatName", IsUnique = true)]
        //[Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ImageFilePath { get; set; }

        public bool Active { get; set; }
    }
}