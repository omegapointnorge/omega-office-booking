namespace server.DAL.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public bool IsAvailable { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();

        public Seat(int id, int roomId, bool isAvailable)
        {
            Id = id;
            RoomId = roomId;
            IsAvailable = isAvailable;
            Bookings = new List<Booking>();
        }
    }
}