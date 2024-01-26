namespace PostOffice.API.DTOs.User
{
    public class UserBaseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime Create_date { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string AccessToken { get; set; }
        public DateTime TokenExpire { get; set; }
        public string? Status { get; set; }
    }
}
