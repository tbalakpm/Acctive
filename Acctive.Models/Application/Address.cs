using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Application
{
    public class Address
    {
        public Address()
        {
            Active = true;
            Companies = new List<Company>();
            Accounts = new List<Accounting.Account>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string ContactName { get; set; }

        [StringLength(50)]
        public string ContactTitle { get; set; }

        [StringLength(100)]
        public string Line1 { get; set; }

        [StringLength(100)]
        public string Line2 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(10)]
        public string Pincode { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(25)]
        public string ContactNumber { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string Email { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Website { get; set; }

        //public string FacebookAccount { get; set; }
        //public string TwitterHandle { get; set; }
        //public string LinkedInAccount { get; set; }

        [Column("AddressType", TypeName = "varchar")]
        [StringLength(15)]
        public string AddressTypeString
        {
            get { return AddressType.ToString(); }
            private set { AddressType = EnumExtensions.ParseEnum<AddressType>(value); }
        }

        [NotMapped]
        public AddressType AddressType { get; set; }

        public bool Active { get; set; }

        public virtual List<Company> Companies { get; set; }
        public virtual List<Accounting.Account> Accounts { get; set; }
    }

    public enum AddressType
    {
        Default,
        Home,
        Office,
        Billing,
        Shipping
    }
}