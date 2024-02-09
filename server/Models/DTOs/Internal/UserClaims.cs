namespace server.Models.DTOs.Internal
{
    public record UserClaims
    {
        public string UserName { get; set; }
        public string Objectidentifier { get; set; }
        public string Email { get; set; }
        public string? Role { get; set; }
        // Constructor overloading
        public UserClaims(string userName, string userId, string role)
            : this(userName, userId, String.Empty, role) { }

        public UserClaims(String name, String id, String email, String? role)
        {
            UserName = name;
            Objectidentifier = id;
            Email = email;
            Role = role;
        }
    }
}