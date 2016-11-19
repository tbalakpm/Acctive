using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Inventory
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }

        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }

        [Column(TypeName = "money")]
        public decimal Quantity { get; set; }

        [ForeignKey("Unit")]
        public int UnitId { get; set; }

        [Column(TypeName = "money")]
        public decimal CostPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal ProfitPercent { get; set; }

        [Column(TypeName = "money")]
        public decimal DiscountPercent { get; set; }

        [Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [Column(TypeName = "money")]
        public decimal TaxPercent { get; set; }

        [Column(TypeName = "money")]
        public decimal Tax { get; set; }

        [Column(TypeName = "money")]
        public decimal SellingPrice { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual Unit Unit { get; set; }
    }
}