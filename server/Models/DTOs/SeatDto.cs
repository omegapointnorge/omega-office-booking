using server.Helpers;
using server.Models.Domain;

namespace server.Models.DTOs
{
    public class SeatDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }

        public bool IsAvailable { get; set; } = true;
        public List<BookingDto> Bookings { get; set; } = new List<BookingDto>();

        public SeatDto(int id, int roomId, List<Booking>? bookings)
        {
            Id = id;
            RoomId = roomId;
            if (bookings != null) Bookings = Mappers.MapBookingDtos(bookings);
        }
    }
}