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
        public DateTime? BookingDateTime_DayOnly { get; set; }

        public int? EventId { get; set; }

        public Event? Event { get; set; }
        public Booking()
        {      
        }
        public Booking(int id, String userId, String userName, int seatId, DateTime bookingDateTime, DateTime? bookingDateTimeDayOnly = null)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
            SeatId = seatId;
            BookingDateTime = bookingDateTime;
            BookingDateTime_DayOnly = bookingDateTimeDayOnly ?? bookingDateTime.Date;
        }
    }
}