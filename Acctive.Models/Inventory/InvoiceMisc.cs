using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Inventory
{
    public class InvoiceMisc
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }

        [Column("MiscType", TypeName = "varchar")]
        [StringLength(15)]
        public string TypeString
        {
            get { return Type.ToString(); }
            private set { Type = EnumExtensions.ParseEnum<MiscType>(value); }
        }

        [NotMapped]
        public MiscType Type { get; set; }

        [Column(TypeName = "money")]
        public decimal Percent { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public virtual Invoice Invoice { get; set; }
    }

    public enum MiscType
    {
        Unknown,
        Discount,
        Tax,
        Surcharge,
        Cess,
        Freight,
        Shipping,
        Parcel,
        Commission
    }
}