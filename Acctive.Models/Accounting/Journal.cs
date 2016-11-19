using Acctive.Models.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Accounting
{
    public class Journal
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Journal code cannot be empty")]
        [Index("IX_JnlCode", IsUnique = true, Order = 1)]
        [StringLength(15)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Journal date cannot be empty")]
        public DateTime Date { get; set; }

        [StringLength(15)]
        public string Reference1 { get; set; }

        [StringLength(15)]
        public string Reference2 { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Column("JournalType", TypeName = "varchar")]
        [StringLength(3)]
        public string JournalTypeString
        {
            get { return JournalType.ToString().Substring(0, 3).ToUpper(); }
            private set { JournalType = EnumExtensions.ParseEnum<JournalType>(value, true); }
        }

        [NotMapped]
        public JournalType JournalType { get; set; }

        [Column(TypeName = "tinyint")]
        public TransactionType TransactionType { get; set; }

        //[Required(ErrorMessage = "Journal account cannot be empty")]
        [ForeignKey("Account")]
        public int AccountId { get; set; }

        [Index("IX_JnlCode", IsUnique = true, Order = 2)]
        [ForeignKey("Period")]
        public int PeriodId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Account Account { get; set; }
        public virtual List<JournalItem> LineItems { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
        public virtual Period Period { get; set; }
    }

    public enum JournalType
    {
        Receipts = 1,
        Payments,
        Deposits,
        Withdrawals,
        Journal,
        Purchase,
        Sales
    }

    public enum TransactionType : byte { Debit = 1, Credit }
}