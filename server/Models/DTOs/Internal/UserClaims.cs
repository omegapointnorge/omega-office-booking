using server.Models.Domain;
using System.Security.Cryptography.Pkcs;

namespace server.Models.DTOs.Internal
{
    public record UserClaims
    {
        public string UserName { get; set; }
        public string Objectidentifier { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        // Constructor overloading
        public UserClaims(string userName, string userId)
            : this(userName, userId, String.Empty, String.Empty) { }

        public UserClaims(String name, String id, String email, String role)
        {
            UserName = name;
            Objectidentifier = id;
            Email = email;
            Role = role;
        }
    }
}