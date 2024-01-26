using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.API.Data.Models
{
    public class MoneyScope
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public float min_value { get; set; }
        public float max_value { get; set;}
        public string? description { get; set; }

        public ICollection<MoneyServicePrice> MoneyServicePrice { get; set; }
    }
}
