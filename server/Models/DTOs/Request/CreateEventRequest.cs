namespace server.Models.DTOs.Request;

public class CreateEventRequest
{
    public string? EventName { get; set; }
    public DateTime BookingDateTime { get; set; }
    public List<int>? SeatList { get; set; }
}


