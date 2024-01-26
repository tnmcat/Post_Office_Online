using PostOffice.API.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.Sockets;

namespace PostOffice.API.DTOs.MoneyOrder
{
    public class MoneyOrderUpdateDTO
    {
        [Required]
        public int id { get; set; }

        public Guid user_id { get; set; }
        public string? sender_name { get; set; }
        public string? sender_pincode { get; set; }
        public string? sender_address { get; set; }
        public string? sender_phone { get; set; }
        public string? sender_national_identity_number { get; set; }

        public string? receiver_name { get; set; }
        public string? receiver_pincode { get; set; }
        public string? receiver_address { get; set; }
        public string? receiver_phone { get; set; }
        public string? receiver_email { get; set; }
        public string? receiver_national_identity_number { get; set; }

        public TransferStatus transfer_status { get; set; }

        public string? note { get; set; }

        public string? payer { get; set; }

        //datetime infor
        public DateTime send_date { get; set; }
        public DateTime? receive_date { get; set; }

        public float transfer_value { get; set; }
        public float transfer_fee { get; set; }

        //charge infor         
        public float total_charge { get; set; }

    }
}
