using OnlineBookstoreMS.Models.Entity;

namespace OnlineBookstoreMS.RespondSchema
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Role = user.Role;
            Token = token;
        }
    }
}
