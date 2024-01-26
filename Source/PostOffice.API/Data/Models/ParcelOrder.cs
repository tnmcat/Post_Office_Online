using PostOffice.API.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.API.Data.Models
{
    public class ParcelOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        //personal infor
        public Guid user_id { get; set; }        
        public string sender_name   { get; set; }
        public string sender_pincode { get; set; }
        public string sender_address { get; set; }
        public string sender_phone { get; set; }
        public string sender_email { get; set; }

        public string receiver_name { get; set; }
        public string receiver_pincode { get; set; }
        public string receiver_address { get; set; }
        public string receiver_phone { get; set; }
        public string receiver_email { get; set; }

        //parcel infor
        public int order_status { get; set; }
        public string? description { get; set; }
        public string? note { get; set; }
        public float parcel_length { get; set; }
        public float parcel_height { get; set; }
        public float parcel_width { get; set; }
        public float parcel_weight { get; set; }

        //service infor
        public int service_id { get; set; }
        public int parcel_type_id { get; set; }

        //payment infor
        public string payer { get; set; }
        public string payment_method { get; set; }

        //datetime infor
        public DateTime? send_date { get; set; }
        public DateTime? receive_date { get; set; }

        //charge infor
        public float vpp_value { get; set; }
        public float total_charge { get; set; }

        public ParcelService? ParcelService { get; set; }
        public ParcelType? ParcelType { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public Pincode? ParcelSenderPincode { get; set; }
        public Pincode? ParcelReceiverPincode { get; set; }

        public AppUser? AppUser { get; set; }
        public ICollection<TrackHistory>? TrackHistories { get; set; }

    }
}
