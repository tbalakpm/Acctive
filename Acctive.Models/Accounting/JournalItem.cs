using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Accounting
{
    public class JournalItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Journal")]
        public int JournalId { get; set; }

        //[Required(ErrorMessage = "Journal account cannot be empty")]
        [ForeignKey("Account")]
        public int AccountId { get; set; }

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

        public virtual Journal Journal { get; set; }
        public virtual Account Account { get; set; }
    }
}