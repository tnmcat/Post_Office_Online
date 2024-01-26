using PostOffice.API.Data.Models;

namespace PostOffice.API.DTOs.ParcelServicePrice
{
    public class ServicePriceExpress
    {
        public int id {  get; set; }
        public string Name { get; set; }
        public decimal ServicePrice { get; set; }
        public string Description { get; set; }
        public string ZoneDescription { get; set; }
    }
}
