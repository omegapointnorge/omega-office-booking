namespace server.Models.DTOs
{
    public class SeatDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string? SeatOwnerUserId { get; set; }

        public SeatDto(int id, int roomId, string seatOwnerUserId)
        {
            Id = id;
            RoomId = roomId;
            SeatOwnerUserId = seatOwnerUserId;
        }
    }
}