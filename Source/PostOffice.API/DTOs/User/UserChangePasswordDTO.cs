using System.ComponentModel.DataAnnotations;

namespace PostOffice.API.DTOs.User
{
    public class UserChangePasswordDTO
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is a required field.")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirm password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
