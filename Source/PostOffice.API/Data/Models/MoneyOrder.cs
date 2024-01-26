using PostOffice.API.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostOffice.API.Data.Models
{
    public class MoneyOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        //personal infor
        public Guid user_id { get; set; }

        public string? sender_name { get; set; }
        public string? sender_pincode { get; set; }
        public string? sender_address { get; set; }
        public string? sender_phone { get; set; }
        public string? sender_email { get; set; }
        public string? sender_national_identity_number { get; set; }

        public string? receiver_name { get; set; }
        public string? receiver_pincode { get; set; }
        public string? receiver_address { get; set; }
        public string? receiver_phone { get; set; }
        public string? receiver_email { get; set; }
        public string? receiver_national_identity_number { get; set; }

        public TransferStatus transfer_status { get; set; }
        public string? note { get; set; }

        public float transfer_value { get; set; }
        public float transfer_fee { get; set; }         

        //fee payment infor
        public string? payer { get; set; }           

        //datetime infor
        public DateTime send_date { get; set; }
        public DateTime? receive_date { get; set; }

        //charge infor         
        public float total_charge { get; set; }


        public Pincode? MoneySenderPincode { get; set; }
        public Pincode? MoneyReceiverPincode { get; set; }
        public AppUser? AppUser { get; set; }             
       /* public MoneyServicePrice MoneyServicePrice { get; set; }*/
    }
}
