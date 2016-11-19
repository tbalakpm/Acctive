using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Application
{
    public class Company
    {
        public Company()
        {
            Active = true;

            Addresses = new List<Address>();
            Periods = new List<Period>();
            Options = new List<CompanyConfig>();

            //Addresses.Add(new Address());
            //Periods.Add(new Period());
        }

        private int _id;
        private string _code;
        private string _name;
        private string _description;
        private string _imageFilePath;
        private string _regdNumber;
        private string _salesTaxNumber;
        private string _cstNumber;
        private bool _active;

        [Key]
        public int Id { get { return _id; } set { if (value != _id) _id = value; } }

        [Required(ErrorMessage = "Company code cannot be empty")]
        [Index("CompanyCode", IsUnique = true)]
        [StringLength(15)]
        public string Code { get { return _code; } set { if (value != _code) _code = value; } }

        [Required(ErrorMessage = "Company name cannot be empty")]
        [Index("CompanyName", IsUnique = true)]
        [StringLength(100)]
        public string Name { get { return _name; } set { if (value != _name) _name = value; } }

        [StringLength(255)]
        public string Description { get { return _description; } set { if (value != _description) _description = value; } }

        [DisplayName("Image")]
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string ImageFilePath { get { return _imageFilePath; } set { if (value != _imageFilePath) _imageFilePath = value; } }

        [DisplayName("Registered No.")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string RegdNumber { get { return _regdNumber; } set { if (value != _regdNumber) _regdNumber = value; } }

        [DisplayName("Sales Tax No.")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string SalesTaxNumber { get { return _salesTaxNumber; } set { if (value != _salesTaxNumber) _salesTaxNumber = value; } }

        [DisplayName("CST No.")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string CstNumber { get { return _cstNumber; } set { if (value != _cstNumber) _cstNumber = value; } }

        public bool Active { get { return _active; } set { if (value != _active) _active = value; } }

        public virtual List<Address> Addresses { get; set; }
        public virtual List<Period> Periods { get; set; }
        public virtual List<CompanyConfig> Options { get; set; }

        //public virtual List<Accounting.Account> Accounts { get; set; }
        //public virtual List<Product> Products { get; set; }
    }
}