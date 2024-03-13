using System.ComponentModel.DataAnnotations;

namespace server.Models.DTOs.Request;

public class CreateEventRequest
{
    [Required(ErrorMessage = "EventName is required")]
    public string? EventName { get; set; }

    public DateTime BookingDateTime { get; set; }

    [Required(ErrorMessage = "SeatList is required")]
    [MinLength(1, ErrorMessage = "SeatList must contain at least one item")]
    public List<int>? SeatList { get; set; }
}