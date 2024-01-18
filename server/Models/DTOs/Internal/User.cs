using server.Models.Domain;

namespace server.Models.DTOs.Internal
{
    public record User
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public User(String name, String id)
        {
            UserName = name;
            UserId = id;
        }
    }
}
