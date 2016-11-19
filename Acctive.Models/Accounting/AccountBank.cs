using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Accounting
{
    public class AccountBank
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bank account number cannot be empty")]
        [Index("BankAcctNum", IsUnique = true, Order = 1)]
        [StringLength(50)]
        public string AccountNumber { get; set; }

        public BankAccountType AccountType { get; set; }

        [StringLength(50)]
        public string IfscCode { get; set; }

        [StringLength(50)]
        public string MicrCode { get; set; }

        [Index("BankAcctNum", IsUnique = true, Order = 2)]
        [ForeignKey("Account")]
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }
    }

    public enum BankAccountType
    {
        Unknown,
        Savings,
        Current,
        Overdraft,
        FixedDeposit
    }
}