

namespace PostOffice.API.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
