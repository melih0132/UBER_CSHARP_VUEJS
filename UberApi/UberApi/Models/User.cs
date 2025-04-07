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

    public class ClientDTO
    {
        public string NomUser { get; set; }
        public string PrenomUser { get; set; }
        public string GenreUser { get; set; }
        public DateOnly DateNaissance { get; set; }
        public string Telephone { get; set; }
        public string EmailUser { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string CodePostal { get; set; }
        public string MotDePasseUser { get; set; }
        public string TypeClient { get; set; } = "Uber";
    }
}
