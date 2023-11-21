namespace server.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SeatId { get; set; }
        public DateTime BookingDateTime { get; set; }

        public BookingDto(int id, int userId, int seatId, DateTime bookingBookingDateTime)
        {
            Id = id;
            UserId = userId;
            SeatId = seatId;
            BookingDateTime = bookingBookingDateTime;
        }
    }
}