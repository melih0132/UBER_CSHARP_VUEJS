namespace UberApi.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string? FullName { get; set; }
        public string Password { get; set; }
        public string? UserRole { get; set; }
    }

    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserResponse
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}
