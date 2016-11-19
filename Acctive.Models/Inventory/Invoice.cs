using Acctive.Models.Accounting;
using Acctive.Models.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Inventory
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Invoice number cannot be empty")]
        [Index("InvoiceCode", IsUnique = true, Order = 1)]
        [Column(TypeName = "varchar")]
        [StringLength(15)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Invoice date cannot be empty")]
        public DateTime Date { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(15)]
        public string Reference1 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(15)]
        public string Reference2 { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string Description { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }

        [ForeignKey("TransactionAccount")]
        public int TranAccountId { get; set; }

        //[Column(TypeName = "money")]
        //public decimal DiscountPercent { get; set; }

        //[Column(TypeName = "money")]
        //public decimal Discount { get; set; }

        //[Column(TypeName = "money")]
        //public decimal Freight { get; set; }

        //[Column(TypeName = "money")]
        //public decimal TaxAmount { get; set; }

        //[Column(TypeName = "money")]
        //public decimal Surcharge { get; set; }

        //[Column(TypeName = "money")]
        //public decimal Commission { get; set; }

        [Column("InvoiceType", TypeName = "varchar")]
        [StringLength(10)]
        public string InvoiceTypeString
        {
            get { return InvoiceType.ToString(); }
            private set { InvoiceType = EnumExtensions.ParseEnum<InvoiceType>(value); }
        }

        [NotMapped]
        public InvoiceType InvoiceType { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Index("InvoiceCode", IsUnique = true, Order = 2)]
        [ForeignKey("Period")]
        public int PeriodId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Account TransactionAccount { get; set; }
        public virtual Period Period { get; set; }

        public virtual List<InvoiceItem> LineItems { get; set; }
        public virtual List<InvoiceMisc> Miscellaneous { get; set; }
        public virtual List<InvoicePaymentReceipt> PaymentReceipts { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
    }

    public enum InvoiceType
    {
        Stock = 1    // 'O'
        , Purchase   // 'P'
        , PurReturn  // 'U'
        , Sales      // 'S'
        , SalReturn  // 'R'
    }
}