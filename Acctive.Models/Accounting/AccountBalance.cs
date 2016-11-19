using Acctive.Models.Application;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Accounting
{
    public class AccountBalance
    {
        [Key]
        public int Id { get; set; }

        [Index("AccountPeriod", 1, IsUnique = true)]
        [ForeignKey("Account")]
        public int AccountId { get; set; }

        [Column(TypeName = "money")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public TransactionType Sign { get; set; }

        [Index("AccountPeriod", 2, IsUnique = true)]
        [ForeignKey("Period")]
        public int PeriodId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Period Period { get; set; }
    }
}