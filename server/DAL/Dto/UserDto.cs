using server.DAL.Models;
using server.Helpers;

namespace server.DAL.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public List<BookingDto>? Bookings { get; set; } = new List<BookingDto>();

        public UserDto(string id, string name, string email, List<Booking>? bookings)
        {
            Id = id;
            Name = name;
            Email = email;
            if (bookings != null) Bookings = Mappers.MapBookingDtos(bookings);
        }
    }
}