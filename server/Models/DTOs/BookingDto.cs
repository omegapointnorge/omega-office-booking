namespace server.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int SeatId { get; set; }
        public DateTime DateTime { get; set; }

        public BookingDto(int id, Guid userId, int seatId, DateTime dateTime)
        {
            Id = id;
            UserId = userId;
            SeatId = seatId;
            DateTime = dateTime;
        }
        public BookingDto(Guid userId, int seatId)
        {
            UserId = userId;
            SeatId = seatId;
        }
    }
}