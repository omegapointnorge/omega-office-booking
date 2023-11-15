using Microsoft.AspNetCore.Identity;
using server.Models.Domain;

namespace server.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SeatId { get; set; }
        public DateTime BookingDateTime { get; set; }
        public User User { get; set; }
        public Seat Seat { get; set; }

        public BookingDto(int id, int userId, int seatId, DateTime bookingDateTime, User user, Seat seat)
        {
            Id = id;
            UserId = userId;
            SeatId = seatId;
            BookingDateTime = bookingDateTime;
            User = user;
            Seat = seat;
        }
        
    }
}