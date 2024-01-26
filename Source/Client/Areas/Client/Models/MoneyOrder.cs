using PostOffice_Server.Models;

namespace PostOffice.Client.Areas.Client.Models
{        public class MoneyOrder
        {
            public int id { get; set; }

            public int user_id { get; set; }
            public string? sender_name { get; set; }
            public string? sender_pincode { get; set; }
            public string? sender_phone { get; set; }
            public string? sender_address { get; set; }

            public string? receiver_name { get; set; }

            public string? receiver_pincode { get; set; }

            public string? receiver_phone { get; set; }

            public string? receiver_address { get; set; }

            public float transfer_value { get; set; }

            public float transfer_fee { get; set; }

            public float total_charge { get; set; }

            public string? note { get; set; }

            public DateTime transfer_date { get; set; }
            public DateTime? receive_date { get; set; }

            public string? transfer_status { get; set; }

            public string? sender_national_identity_number { get; set; }

            public string? receiver_national_identity_number { get; set; }

            public virtual User? user { get; set; }
        }
    }



