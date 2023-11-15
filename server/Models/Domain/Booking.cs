namespace server.Models.Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SeatId { get; set; }
        public DateTime BookingDateTime { get; set; }
        public User User { get; set; } = null!;
        public Seat Seat { get; set; } = null!;

        public Booking(int id, int userId, int seatId, DateTime bookingDateTime)
        {
            Id = id;
            UserId = userId;
            SeatId = seatId;
            BookingDateTime = bookingDateTime;
        }
    }
}