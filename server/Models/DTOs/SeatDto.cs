using server.Models.Domain;
namespace server.Models.DTOs
{
    public class SeatDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
        
        public SeatDto(int id, int roomId, Room room, ICollection<Booking>? bookings)
        {
            Id = id;
            RoomId = roomId;
            Room = room;
            Bookings = bookings;
        }
    }
}