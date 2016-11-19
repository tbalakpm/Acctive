using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Inventory
{
    public class Warehouse
    {
        public Warehouse()
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

        [DisplayName("Image Url")]
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ImageFilePath { get; set; }

        public bool Active { get; set; }
    }
}