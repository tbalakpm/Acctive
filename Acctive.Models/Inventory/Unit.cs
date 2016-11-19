using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Inventory
{
    public class Unit
    {
        public Unit()
        {
            Active = true;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Code cannot be empty")]
        [Index("IX_Code", IsUnique = true, Order = 1)]
        [StringLength(15)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        [Index("IX_Name", IsUnique = true, Order = 1)]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        //[Column(TypeName = "money")]
        //public decimal ConversionFactor { get; set; }

        //[Display(Name = "Parent Unit")]
        //[ForeignKey("Parent")]
        //public int? ParentId { get; set; }

        //public int LevelNumber { get; set; }
        //public int IndexNumber { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ImageFilePath { get; set; }

        //[Index("IX_UnitCode", IsUnique = true, Order = 2)]
        //[Index("IX_UnitName", IsUnique = true, Order = 2)]
        //[ForeignKey("Company")]
        //public int CompanyId { get; set; }

        public bool Active { get; set; }

        //public virtual Unit Parent { get; set; }
        //public virtual Company Company { get; set; }
        //public virtual List<Unit> Children { get; set; }
        //public virtual List<Product> Products { get; set; }
    }
}