using server.Helpers;
using server.Models.Domain;

namespace server.Models.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Email { get; set; } = string.Empty;
        public List<BookingDto>? Bookings { get; set; } = new List<BookingDto>();

        public UserDto(int id, string name, string email, List<Booking> bookings)
        {
            Id = id;
            Name = name;
            Email = email;
            Bookings = Mappers.MapBookingDtos(bookings);
        }
    }
}