using Microsoft.AspNetCore.Identity;
using server.Models.Domain;
using server.Services;

namespace server.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SeatId { get; set; }
        public DateTime BookingDateTime { get; set; }
        public User User { get; set; }
        public SeatDto Seat { get; set; }

        public BookingDto(int id, int userId, int seatId, Seat seat)
        {
            Id = id;
            UserId = userId;
            SeatId = seatId;
            BookingDateTime = DateTime.Now;
            Seat = new SeatDto(seat.Id, seat.RoomId);
        }
        
    }
}