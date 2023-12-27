using server.Helpers;
using server.Models.Domain;

namespace server.Request
{
    public class UserBookingRequest
    {
        public required int SeatId { get; set; }

    }
}