using server.Models.Domain;

namespace server.Models.DTOs
{
    public class HistoryBookingDto
    {
        public int Id { get; set; }
        public int[] SeatIds { get; set; }
        public int[] RoomIds { get; set; }
        public string? EventName { get; internal set; }
        public int? EventId { get; internal set; }

        public DateTime BookingDateTime { get; set; }

        public HistoryBookingDto(Booking booking)
        {
            Id = booking.Id;
            SeatIds = [booking.SeatId];
            RoomIds = [booking.Seat.RoomId];
            EventName = booking.Event?.Name;
            EventId = booking.EventId;
            BookingDateTime = booking.BookingDateTime;
        }
        public HistoryBookingDto(int EventId, int[] seatIds, int[] roomIds, string eventName, DateTime bookingDateTime)
        {
            Id = EventId;
            SeatIds = seatIds;
            RoomIds = roomIds;
            EventName = eventName;
            BookingDateTime = bookingDateTime;
        }
    }
}
