using server.Models.Domain;

namespace server.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public String UserId { get; set; }
        public String UserName { get; set; }

        public int SeatId { get; set; }
        public String BookingDateTime { get; set; }

        public BookingDto(Booking booking) {
            Id = booking.Id;
            UserId = booking.UserId;
            UserName = booking.UserName;
            SeatId = booking.SeatId;
            BookingDateTime = booking.BookingDateTime.ToUniversalTime().ToString("o");
        }
    }
}