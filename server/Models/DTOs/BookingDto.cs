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
            // Convert to Norwegian time zone
            Id = id;
            UserId = userId;
            SeatId = seatId;
            BookingDateTime = dateTime;
        }
    }
}