using System.ComponentModel.DataAnnotations;

namespace PostOffice.API.DTOs.User
{
    public class UserUpdateDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Valid phone number must has 10 to 15 number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string PincodeId { get; set; }
        [Required]
        public string Address { get; set; }            

    }
}
