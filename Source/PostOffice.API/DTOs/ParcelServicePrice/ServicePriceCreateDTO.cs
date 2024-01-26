namespace PostOffice.API.DTOs.ParcelServicePrice
{
    public class ServicePriceCreateDTO
    {
        public int zone_type_id { get; set; }
        public int service_id { get; set; }
        public int parcel_type_id { get; set; }
        public int scope_weight_id { get; set; }
        public float service_price { get; set; }
    }
}
