

namespace PostOffice.API.DTOs.ParcelService
{
    public class ParcelServiceBaseDTO
    {
        public int service_id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }

        public bool status { get; set; }

        public int? delivery_time { get; set; }
    }
}
