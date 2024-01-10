namespace server.DAL.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SeatId { get; set; }
        public User User { get; set; } = null!;
        public Seat Seat { get; set; } = null!;
        public DateTime BookingDateTime { get; set; }
        public Booking()
        {
                UserId = string.Empty;
        }
        public Booking(int id, string userId, int seatId, DateTime bookingDateTime)
        {
            Id = id;
            UserId = userId;
            SeatId = seatId;
            BookingDateTime = bookingDateTime;
        }
    }
}