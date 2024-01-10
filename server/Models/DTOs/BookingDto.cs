using server.Models.Domain;

namespace server.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public String UserId { get; set; }
        public int SeatId { get; set; }
        public String BookingDateTime { get; set; }

        public BookingDto(int id, String userId, int seatId, DateTime dateTime)
        {
            // Time zone identifier for Norway (with DST information)
            string norwayTimeZoneId = "Central Europe Standard Time";

            // Convert to Norwegian time zone
            Id = id;
            UserId = userId;
            SeatId = seatId;
            BookingDateTime = dateTime.ToUniversalTime().ToString("o");
        }

        public BookingDto(Booking booking) {
            Id = booking.Id;
            UserId = booking.UserId;
            SeatId = booking.SeatId;
            BookingDateTime = booking.BookingDateTime.ToUniversalTime().ToString("o");
        }

        static String ConvertToTimeZone(DateTime originalDateTime, string timeZoneId)
        {
            // Get the time zone information
            TimeZoneInfo norwayTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            // Convert the DateTime to the specified time zone
            DateTime convertedDateTime = TimeZoneInfo.ConvertTime(originalDateTime, norwayTimeZone);

            return convertedDateTime.ToString("dd/MM/yyyy HH:mm");
        }
    }
}