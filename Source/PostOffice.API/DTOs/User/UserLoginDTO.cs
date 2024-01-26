using System.ComponentModel.DataAnnotations;

namespace PostOffice.API.DTOs.User
{
    public class UserLoginDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public bool RememberMe { get; set; }            

    }
}
