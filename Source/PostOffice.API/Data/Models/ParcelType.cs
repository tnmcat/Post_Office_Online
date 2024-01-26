using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.API.Data.Models
{
    public class ParcelType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float max_length { get; set; }
        public float max_width { get; set; }
        public float max_height { get; set; }        
        public float over_dimension_rate { get; set; }

        public ICollection<ParcelServicePrice>? ParcelServicePrice { get; set; }
        public ICollection<ParcelOrder>? ParcelOrders { get; set; }
    }
}
