using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.API.Data.Models
{
    public class MoneyServicePrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int zone_type_id { get; set; }
        public int money_scope_id { get; set; }
        public float fee { get; set; }

        public MoneyScope MoneyScopes { get; set; }
        public ZoneType? ZoneTypes { get; set; }
    }
}
