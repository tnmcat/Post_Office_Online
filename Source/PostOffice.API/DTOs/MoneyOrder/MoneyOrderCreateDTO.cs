using PostOffice.API.Data.Enums;

namespace PostOffice.API.DTOs.MoneyOrder
{
    public class MoneyOrderCreateDTO
    {

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

        //datetime infor
        public DateTime send_date { get; set; }
        public DateTime? receive_date { get; set; }

        public string? payer { get; set; }

        //money order infor

        public float transfer_value { get; set; }
        public float transfer_fee { get; set; }

        //charge infor         
        public float total_charge { get; set; }
    }
}
