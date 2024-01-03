using server.Helpers;
using server.Models.Domain;

namespace server.Request
{
    //In the future we may need to add booked time and other paramters here. so we keep it as seperate Request class
    public class UserBookingRequest
    {
        public required int SeatId { get; set; }

    }
}