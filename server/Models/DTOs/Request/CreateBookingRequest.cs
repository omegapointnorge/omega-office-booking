namespace server.Models.DTOs.Request
{
    public class CreateBookingRequest
    {
        public required int SeatId { get; set; }

        public DateTime BookingDateTime { get; set; }

        public List<int>? SeatList { get; set; }

        public bool? IsEvent { get; set; }
    }
}