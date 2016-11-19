using System.ComponentModel.DataAnnotations;

namespace MyData.Application
{
    public class CompanyAddress
    {
        [Key]
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public int AddressId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Address Address { get; set; }
    }
}