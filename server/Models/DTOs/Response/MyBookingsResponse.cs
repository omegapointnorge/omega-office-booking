
namespace server.Models.DTOs.Response
{
    public class MyBookingsResponse : BookingDto
    {
        public MyBookingsResponse(int id, string userId, int seatId, DateTime dateTime, int roomId) : base(id, userId, seatId, dateTime)
        {
            RoomId = roomId;
        }
        public int RoomId { get; set; }
    }
}
