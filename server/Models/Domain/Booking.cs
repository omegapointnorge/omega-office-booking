namespace server.Models.Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int SeatId { get; set; }
        public User User { get; set; } = null!;
        public Seat Seat { get; set; } = null!;
        public DateTime BookingDateTime { get; set; }
        public Booking()
        {
         
        }
        public Booking(Guid userId, int seatId)
        {
            UserId = userId;
            SeatId = seatId;
        }
        public Booking(int id, Guid userId, int seatId, DateTime bookingDateTime)
        {
            Id = id;
            UserId = userId;
            SeatId = seatId;
            BookingDateTime = bookingDateTime;
        }
    }
}