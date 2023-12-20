namespace server.Models.Domain
{
    public class Seat
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Boolean IsAvailable { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        
        public Seat(int id, int roomId, Boolean isAvailable)
        {
            Id = id;
            RoomId = roomId;
            IsAvailable = isAvailable;
            Bookings = new List<Booking>();
        }

        public Seat()
        {
        }
    }
}