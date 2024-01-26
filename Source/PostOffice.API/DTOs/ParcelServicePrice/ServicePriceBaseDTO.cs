using PostOffice.API.DTOs.WeightScope;

namespace PostOffice.API.DTOs.ParcelServicePrice
{
    public class ServicePriceBaseDTO
    {
        public int parcel_price_id { get; set; }
        public int zone_type_id { get; set; }
        public int service_id { get; set; }
        public int parcel_type_id { get; set; }
        public int scope_weight_id { get; set; }
        public float service_price { get; set; }

        public WeightScopeBaseDTO WeightScope { get; set; }

    }
}
