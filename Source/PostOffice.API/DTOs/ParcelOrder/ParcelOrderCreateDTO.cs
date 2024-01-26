using PostOffice.API.Data.Enums;
using PostOffice.API.DTOs.Pincode;
using System.ComponentModel.DataAnnotations;

namespace PostOffice.API.DTOs.ParcelOrder
{
    public class ParcelOrderCreateDTO
    {

         
        public Guid user_id { get; set; }
        
        public string? sender_name { get; set; }
        public string? sender_pincode { get; set; }
    

        public string? sender_address { get; set; }
        
        public string? sender_phone { get; set; }
        public string? sender_email { get; set; }
        [Required]
        public string? description { get; set; }
        [Required]
        public string? note { get; set; }
        [Required]

        public string? receiver_name { get; set; }
        [Required]
        public string? receiver_pincode { get; set; }
        [Required]

        public string? receiver_address { get; set; }
        [Required]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Valid phone number must has 10 to 15 number")]
        public string? receiver_phone { get; set; }
        [Required, EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string? receiver_email { get; set; }
        [Required]
        public float? parcel_length { get; set; }
        [Required]
        public float? parcel_height { get; set; }
        [Required]
        public float? parcel_width { get; set; }
        [Required]
        public float? parcel_weight { get; set; }
        [Required]
        public string? payer { get; set; }
        [Required]
        public string? payment_method { get; set; }
        [Required]
        public int service_id { get; set; }
        [Required]
        public int parcel_type_id { get; set; }
        public int order_status { get; set; }
        public DateTime send_date { get; set; }
        public DateTime receive_date { get; set; }

        public float total_charge { get; set; }
        //public float total_charge { get; set; }
        
    }
}
