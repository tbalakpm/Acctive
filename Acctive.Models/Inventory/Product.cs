using Acctive.Models.Application;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Inventory
{
    public class Product
    {
        public Product()
        {
            Active = true;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(15)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }

        //public int LevelNumber { get; set; }
        //public int IndexNumber { get; set; }
        public bool IsGroup { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        [ForeignKey("Unit")]
        public int UnitId { get; set; }

        [Column(TypeName = "money")]
        public decimal? CostPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal? ProfitPercent { get; set; }

        [Column(TypeName = "money")]
        public decimal? SellingPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal? TaxPercent { get; set; }

        [Column(TypeName = "money")]
        public decimal? Surcharge { get; set; }

        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }

        [Column(TypeName = "money")]
        public decimal? MinimumQuantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? MaximumQuantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? ReorderLevelQuantity { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ImageFilePath { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public bool Active { get; set; }

        public virtual Product Parent { get; set; }
        public virtual ProductCategory Category { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Company Company { get; set; }

        public virtual List<Product> Children { get; set; }
        public virtual List<Inventory> Inventories { get; set; }
    }
}