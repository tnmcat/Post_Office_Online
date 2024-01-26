using System.ComponentModel.DataAnnotations;

namespace PostOffice.API.DTOs.User
{
    public class UserResetPasswordDTO
    {
        [Required]
        public string Password { get; set; } = null;

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; } = null;

        public string Token { get; set; }

        public string Email { get; set; }
    }
}
