namespace server.Models.Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public String UserId { get; set; }
        public String UserName { get; set; }
        public int SeatId { get; set; }
        public Seat Seat { get; set; } = null!;
        public DateTime BookingDateTime { get; set; }
        public Booking()
        {
            UserId = string.Empty;
            UserName = string.Empty;
        }
        public Booking(int id, String userId, String userName, int seatId, DateTime bookingDateTime)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
            SeatId = seatId;
            BookingDateTime = bookingDateTime;
        }
    }
}