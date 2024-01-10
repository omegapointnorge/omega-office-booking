using server.DAL.Models;

namespace server.DAL.Dto
{
    public class CreateBookingResponse
    {
        public int SeatId { get; set; }
        public string UserId { get; set; }
        public DateTime BookingDateTime { get; set; }

        public CreateBookingResponse(Booking booking)
        {
            SeatId = booking.SeatId;
            UserId = booking.UserId;
            BookingDateTime = booking.BookingDateTime;
        }

    }
}