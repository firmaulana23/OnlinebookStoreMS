using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBookstoreMS.Models.Entity
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string PasswordHash { get; set; }
        public required string Role { get; set; }

        [NotMapped]
        public string Token { get; set; }

    }
}