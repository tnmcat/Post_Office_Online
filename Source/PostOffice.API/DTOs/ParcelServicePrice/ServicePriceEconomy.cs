using PostOffice.API.Data.Models;
using PostOffice.API.DTOs.WeightScope;

namespace PostOffice.API.DTOs.ParcelServicePrice
{
    public class ServicePriceEconomy
    {
        public int id {  get; set; }
        public float service_price { get; set; }
        public WeightScopeBaseDTO weightScope{ get; set; }
        public ZoneType ZoneType { get; set; }
    }
}
