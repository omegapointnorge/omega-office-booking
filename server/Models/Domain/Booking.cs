namespace server.Models.Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public String UserId { get; set; }
        public String UserName { get; set; }
        public int SeatId { get; set; }
        public int? EventId { get; set; }
        public DateTime BookingDateTime { get; set; }
        public DateTime BookingDateTime_DayOnly { get; set; }

        //associated model
        public Seat Seat { get; set; } = null!;
        public Event Event { get; set; }

        public Booking(int id, String userId, String userName, int seatId,  DateTime bookingDateTime, DateTime bookingDateTimeDayOnly, int? eventID)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
            SeatId = seatId;
            BookingDateTime = bookingDateTime;
            BookingDateTime_DayOnly = bookingDateTimeDayOnly;
            EventId = eventID;
        }
        public Booking(String userId, String userName, int seatId, DateTime bookingDateTime, DateTime bookingDateTimeDayOnly, int? eventID)
            : this(0, userId, userName, seatId, bookingDateTime, bookingDateTimeDayOnly, eventID)
        {
        }
        // booking with Bookingdatetime not for event
        public Booking(String userId, String userName, int seatId, DateTime bookingDateTime, DateTime bookingDateTimeDayOnly )
       : this(0, userId, userName, seatId, bookingDateTime, bookingDateTimeDayOnly, null)
        { 
        }
            public Booking(String userId, String userName, int seatId, DateTime bookingDateTime)
            : this(0, userId, userName, seatId, bookingDateTime, bookingDateTime.Date,null)
        {
        }
    }
}