
using server.Models.Domain;

namespace server.Models.DTOs.Response
{
    public class MyBookingsResponse : BookingDto
    {
        public MyBookingsResponse(Booking booking) : base(booking)
        {
            RoomId = booking.Seat.RoomId;
        }
        public int RoomId { get; set; }
    }
}
