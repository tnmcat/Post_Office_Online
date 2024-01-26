using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.API.Data.Models
{
    public class ZoneType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string? zone_description { get; set; }
        public ICollection<ParcelServicePrice>? ParcelServicePrice { get; set; }
        public ICollection<MoneyServicePrice>? MoneyServicePrice { get; set; }

        public static implicit operator ZoneType(int v)
        {
            throw new NotImplementedException();
        }
    }
}
