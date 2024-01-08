namespace server.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public String UserId { get; set; }
        public int SeatId { get; set; }
        public DateTime BookingDateTime { get; set; }

        public BookingDto(int id, String userId, int seatId, DateTime dateTime)
        {
            // Time zone identifier for Norway (with DST information)
            string norwayTimeZoneId = "Central Europe Standard Time";

            // Convert to Norwegian time zone
            Id = id;
            UserId = userId;
            SeatId = seatId;
            BookingDateTime = DateTime.Parse(ConvertToTimeZone(dateTime, norwayTimeZoneId));
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