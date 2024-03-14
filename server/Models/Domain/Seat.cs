namespace server.Models.Domain
{
    public class Seat
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public string? SeatOwnerEmail { get; set; }

        public Seat(int id, int roomId, string? seatOwnerEmail)
        {
            Id = id;
            RoomId = roomId;
            SeatOwnerEmail = seatOwnerEmail;
        }
    }
}