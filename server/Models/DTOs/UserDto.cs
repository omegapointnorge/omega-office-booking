
using server.Models.Domain;

namespace server.Models.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Email { get; set; } = string.Empty;
        public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();

        public UserDto(int id, string name, string email, ICollection<Booking>? bookings)
        {
            Id = id;
            Name = name;
            Email = email;
            Bookings = bookings;
        }
    }
}