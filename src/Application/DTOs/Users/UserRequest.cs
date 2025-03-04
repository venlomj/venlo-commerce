
namespace Application.DTOs.Users
{
    public class UserRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
    }
}
