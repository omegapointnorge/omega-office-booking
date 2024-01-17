using server.Models.Domain;

namespace server.Models.DTOs.Response
{
    public class CreateBookingResponse
    {
        public int Id { get; set; }
        public String UserId { get; set; }
        public int SeatId { get; set; }
        public DateTime BookingDateTime { get; set; }
        
        public CreateBookingResponse(Booking booking) {
            Id = booking.Id;
            SeatId = booking.SeatId;
            UserId = booking.UserId;
            BookingDateTime = booking.BookingDateTime;
        }

    }
}