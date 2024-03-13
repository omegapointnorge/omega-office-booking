using System.ComponentModel.DataAnnotations;

namespace server.Models.DTOs.Request
{
    public class CreateBookingRequest
    {
        public int SeatId { get; set; }

        public DateTime BookingDateTime { get; set; }

        public string? reCAPTCHAToken { get; set; }

    }
}