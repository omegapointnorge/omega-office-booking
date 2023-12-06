using server.Helpers;
using server.Models.Domain;

namespace server.Request
{
    public class UserBookingRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required int SeatId { get; set; }

    }
}