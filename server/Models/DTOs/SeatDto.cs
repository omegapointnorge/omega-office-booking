namespace server.Models.DTOs
{
    public class SeatDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }

        public SeatDto(int id, int roomId)
        {
            Id = id;
            RoomId = roomId;
        }
    }
}