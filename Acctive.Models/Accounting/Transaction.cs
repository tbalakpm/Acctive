using Acctive.Models.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Accounting
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        //[Column(TypeName = "varchar")]
        //[StringLength(15)]
        //public string Code { get; set; }

        [Required(ErrorMessage = "Transaction date cannot be empty")]
        public DateTime Date { get; set; }

        [ForeignKey("DebitAccount")]
        public int DebitAccountId { get; set; }

        [ForeignKey("CreditAccount")]
        public int CreditAccountId { get; set; }

        [Column(TypeName = "money")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(15)]
        public string Reference1 { get; set; }

        [StringLength(15)]
        public string Reference2 { get; set; }

        [ForeignKey("Period")]
        public int PeriodId { get; set; }

        public virtual Account DebitAccount { get; set; }
        public virtual Account CreditAccount { get; set; }
        public virtual Period Period { get; set; }

        public virtual List<Journal> Journals { get; set; }
        public virtual List<Inventory.Invoice> Invoices { get; set; }
    }
}