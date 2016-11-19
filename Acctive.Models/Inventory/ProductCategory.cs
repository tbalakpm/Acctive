using Acctive.Models.Application;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Inventory
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            Active = true;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product category code cannot be empty")]
        [Index("ProdCatCode", IsUnique = true, Order = 1)]
        [StringLength(15)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Product category name cannot be empty")]
        [Index("ProdCatName", IsUnique = true, Order = 1)]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ImageFilePath { get; set; }

        [Index("ProdCatCode", IsUnique = true, Order = 2)]
        [Index("ProdCatName", IsUnique = true, Order = 2)]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public bool Active { get; set; }

        public virtual Company Company { get; set; }
        public List<Product> Products { get; set; }
    }
}