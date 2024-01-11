using server.Models.Domain;

namespace server.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public String UserId { get; set; }
        public int SeatId { get; set; }
        public String BookingDateTime { get; set; }

        public BookingDto(int id, String userId, int seatId, DateTime dateTime)
        {
            // Convert to Norwegian time zone
            Id = id;
            UserId = userId;
            SeatId = seatId;
            BookingDateTime = dateTime.ToUniversalTime().ToString("o");
        }

        public BookingDto(Booking booking) {
            Id = booking.Id;
            UserId = booking.UserId;
            SeatId = booking.SeatId;
            BookingDateTime = booking.BookingDateTime.ToUniversalTime().ToString("o");
        }
    }
}