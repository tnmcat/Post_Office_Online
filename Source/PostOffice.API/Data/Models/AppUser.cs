using Microsoft.AspNetCore.Identity;

namespace PostOffice.API.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Create_date { get; set; }

        public string PhoneNumber { get; set; }
        public string PincodeId { get; set; }
        public string Address { get; set; }          
        public string? Status { get; set; }

        public Pincode Pincode { get; set; }

        public ICollection<MoneyOrder>? MoneyOrders { get; set; }

        public ICollection<ParcelOrder>? ParcelOrders { get; set; }

        public ICollection<HistoryEmployee>? HistoryEmployees { get; set; }


    }
}
