using server.Helpers;
using server.Models.Domain;

namespace server.Models.DTOs
{
    public class SeatDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; } = null!;
        public List<BookingDto> Bookings { get; set; } = new List<BookingDto>();
        
        public SeatDto(int id, int roomId, List<Booking>? bookings)
        {
            Id = id;
            RoomId = roomId;
            Bookings = Mappers.MapBookingDtos(bookings);
        }
    }
}