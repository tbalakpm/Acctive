using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Inventory
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [StringLength(15)]
        public string Code { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpiryDate { get; set; }

        [Column(TypeName = "money")]
        public decimal Quantity { get; set; }

        //[Column(TypeName = "money")]
        //public decimal CostPrice { get; set; }

        [ForeignKey("Unit")]
        public int UnitId { get; set; }

        //[Column(TypeName = "money")]
        //public decimal SellingPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Unit Unit { get; set; }
    }
}