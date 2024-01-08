using server.Helpers;
using server.Models.Domain;

namespace server.Models.DTOs.Request
{
    public class CreateBookingRequest
    {
        public required int SeatId { get; set; }
    }
}