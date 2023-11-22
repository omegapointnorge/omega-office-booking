namespace server.Models.Domain
{
    public class Seat
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        
        public Seat(int roomId)
        {
            RoomId = roomId;
            Bookings = new List<Booking>();
        }
    }
}