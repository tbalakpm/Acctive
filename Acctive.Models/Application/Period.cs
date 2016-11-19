using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Application
{
    public class Period
    {
        public Period()
        {
            Active = true;

            DateTime today = DateTime.Today;
            if (today.Month >= 4 && today.Month <= 12)
            {
                Code = string.Format("AY{0:yyyy}-{1:yy}", today, today.AddYears(1));
                BeginDate = new DateTime(today.Year, 4, 1);
                EndDate = new DateTime(today.Year + 1, 3, 31);
            }
            else
            {
                Code = string.Format("AY{0:yyyy}-{1:yy}", today.AddYears(-1), today);
                BeginDate = new DateTime(today.Year - 1, 4, 1);
                EndDate = new DateTime(today.Year, 3, 31);
            }
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Period code cannot be empty")]
        [Index("IX_PeriodCode", IsUnique = true, Order = 2)]
        [Column(TypeName = "varchar")]
        [StringLength(15)]
        public string Code { get; set; }

        [Column(TypeName = "date")]
        public DateTime BeginDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public bool Active { get; set; }

        [ForeignKey("Company")]
        [Index("IX_PeriodCode", IsUnique = true, Order = 1)]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        //public virtual List<AccountBalance> Balances { get; set; }
        //public virtual List<Journal> Journals { get; set; }
    }
}