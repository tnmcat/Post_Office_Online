using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace PostOffice.API.Data.Models
{
    public class Pincode
    {
       
        public string? pincode { get; set; }
        public string? city_name { get; set; }
        public int area_id { get; set;}
        public Area? Area { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<OfficeBranch>? OfficeBranches { get; set; }
        public virtual ICollection<ParcelOrder>? SenderPincodePO { get; set; }
        public virtual ICollection<ParcelOrder>? ReceiverPincodePO { get; set; }

        public virtual ICollection<MoneyOrder>? SenderPincodeMO { get; set; }
        public virtual ICollection<MoneyOrder>? ReceiverPincodeMO { get; set; }

    }
}
