using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.API.Data.Models
{
    public class WeightScope
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public float min_weight { get; set; }
        public float max_weight { get; set;}
        public string description { get; set; }
        public ICollection<ParcelServicePrice>? ParcelServicePrice { get; set; }
    }
}
