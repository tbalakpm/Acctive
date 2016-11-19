using Acctive.Models.Application;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Accounting
{
    public class Account
    {
        public Account()
        {
            Active = true;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(15)]
        public string Code { get; set; }

        [Required(ErrorMessage = "Account name cannot be empty")]
        [Index("IX_Name", IsUnique = true, Order = 1)]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }

        //public int LevelNumber { get; set; }
        public int? SequenceNumber { get; set; }

        public bool IsGroup { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ImageFilePath { get; set; }

        [Index("IX_Name", IsUnique = true, Order = 2)]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public bool Active { get; set; }

        public virtual List<Address> Addresses { get; set; }
        public virtual Account Parent { get; set; }
        public virtual AccountCategory Category { get; set; }
        public virtual List<AccountBank> Banks { get; set; }
        public virtual List<AccountBalance> Balances { get; set; }
        public virtual Company Company { get; set; }

        public virtual List<Account> Children { get; set; }
    }

    //public enum LedgerType
    //{
    //    Income,
    //    Expense,
    //    Asset,
    //    Liability,
    //    Purchase,
    //    PurchaseReturn,
    //    Sales,
    //    SalesReturn,
    //    Stock,
    //    RawMaterial,
    //    Branch,
    //    Customer,
    //    Supplier,
    //    Cash,
    //    Bank
    //}
}