namespace server.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SeatId { get; set; }
        public String DateTime { get; set; }

        public BookingDto(int id, int userId, int seatId, DateTime dateTime)
        {
            // Time zone identifier for Norway (with DST information)
            string norwayTimeZoneId = "Egypt Standard Time";

            // Convert to Norwegian time zone
            Id = id;
            UserId = userId;
            SeatId = seatId;
            DateTime = ConvertToTimeZone(dateTime, norwayTimeZoneId);  
        }
        public BookingDto(int userId, int seatId)
        {
            UserId = userId;
            SeatId = seatId;
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