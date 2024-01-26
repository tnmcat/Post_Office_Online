using PostOffice.API.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace PostOffice.API.DTOs.ParcelService
{
    public class ParcelServiceCreateDTO
    {
        [Required]
        public string? name { get; set; }
        public string description { get; set; }
        [Required]
        public bool status { get; set; }

        [Required]
        public int delivery_time { get; set; }
    }
}
