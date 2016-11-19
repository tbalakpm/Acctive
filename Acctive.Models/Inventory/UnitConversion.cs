using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acctive.Models.Inventory
{
    public class UnitConversion
    {
        public UnitConversion()
        {
            Active = true;
        }

        [Key]
        public int Id { get; set; }

        [ForeignKey("FromUnit")]
        [Index("IX_PrimaryKey", IsUnique = true, Order = 1)]
        public int FromUnitId { get; set; }

        [ForeignKey("ToUnit")]
        [Index("IX_PrimaryKey", IsUnique = true, Order = 2)]
        public int ToUnitId { get; set; }

        [Index("IX_PrimaryKey", IsUnique = true, Order = 3)]
        public int CalcStep { get; set; }

        [Column(TypeName = "char")]
        [StringLength(1)]
        public string Operator { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Factor { get; set; }

        public bool Active { get; set; }

        public virtual Unit FromUnit { get; set; }
        public virtual Unit ToUnit { get; set; }
    }
}