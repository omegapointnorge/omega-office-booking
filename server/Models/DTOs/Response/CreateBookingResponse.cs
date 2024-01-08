using server.Models.Domain;

namespace server.Models.DTOs.Response
{
    public class CreateBookingResponse
    {
        public int SeatId { get; set; }
        public String UserId { get; set; }
        public DateTime BookingDateTime { get; set; }
        
        public CreateBookingResponse(Booking booking) {
            SeatId = booking.SeatId;
            UserId = booking.UserId;
            BookingDateTime = booking.BookingDateTime;
        }

    }
}