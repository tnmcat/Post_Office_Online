using PostOffice.API.DTOs.ParcelOrder;
using PostOffice.API.DTOs.Pincode;

namespace PostOffice.API.DTOs
{
    public class Calculation
    {
        public string sender_pincode { get; set; }
        public string receiver_pincode { get; set; }
        public float weight { get; set; }
        

        public int service_id { get; set; }
        public int parcel_type_id { get; set; }
    }
}
