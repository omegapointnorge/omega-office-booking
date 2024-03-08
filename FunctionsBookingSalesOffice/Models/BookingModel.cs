using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionsD365.Models
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


        public Booking(int id, String userId, String userName, int seatId, DateTime bookingDateTime, DateTime bookingDateTimeDayOnly, int? eventID)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
            SeatId = seatId;
            BookingDateTime = bookingDateTime;
            BookingDateTime_DayOnly = bookingDateTimeDayOnly;
            EventId = eventID;
        }

        // booking with Bookingdatetime not for event
        public Booking(String userId, String userName, int seatId, DateTime bookingDateTime, DateTime bookingDateTimeDayOnly)
       : this(0, userId, userName, seatId, bookingDateTime, bookingDateTimeDayOnly, null)
        {
        }
        public Booking(String userId, String userName, int seatId, DateTime bookingDateTime)
        : this(0, userId, userName, seatId, bookingDateTime, bookingDateTime.Date, null)
        {
        }
    }
}
