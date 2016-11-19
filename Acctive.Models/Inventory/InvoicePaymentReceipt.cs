using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Inventory
{
    public class InvoicePaymentReceipt
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }

        [Column("Mode", TypeName = "varchar")]
        [StringLength(15)]
        public string ModeString
        {
            get { return Mode.ToString(); }
            private set { Mode = EnumExtensions.ParseEnum<PaymentReceiptMode>(value); }
        }

        [NotMapped]
        public PaymentReceiptMode Mode { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(25)]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(15)]
        public string ReferenceNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReferenceDate { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public virtual Invoice Invoice { get; set; }
    }

    public enum PaymentReceiptMode
    {
        Unknown,
        Cash,
        CreditCard,
        DebitCard,
        Cheque,
        DemandDraft,
        Coupon
    }
}