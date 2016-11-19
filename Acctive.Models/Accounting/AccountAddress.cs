using MyData.Application;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyData.Accounting
{
    public class AccountAddress
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Address Address { get; set; }
    }
}