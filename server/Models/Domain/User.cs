

using System.ComponentModel.DataAnnotations;

namespace server.Models.Domain
{
    public class User
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}