namespace server.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SeatId { get; set; }
        public DateTime DateTime { get; set; }

        public BookingDto(int id, int userId, int seatId, DateTime dateTime)
        {
            Id = id;
            UserId = userId;
            SeatId = seatId;
            DateTime = dateTime;
        }
        public BookingDto(int userId, int seatId)
        {
            UserId = userId;
            SeatId = seatId;
        }
    }
}